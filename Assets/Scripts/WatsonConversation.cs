using System.Collections;
using System.Collections.Generic;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK.Utilities;
using IBM.Watson.Assistant.V2;
using IBM.Watson.Assistant.V2.Model;
using UnityEngine;

namespace WatsonAssistant
{
    public delegate void ConversationResponseDelegate(
    List<string> messages, string nameEntity, string valueEntity, string nameIntent);

  public class WatsonConversation : MonoBehaviour
  {
    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [Header("Credentials")]
    [Space(10)]
    [Tooltip("The IAM apikey.")]
    [SerializeField]
    private string iamApikey;
    [Tooltip("The service URL (optional). This defaults to \"https://gateway.watsonplatform.net/assistant/api\"")]
    [SerializeField]
    private string serviceUrl;
    [Tooltip("The version date with which you would like to use the service in the form YYYY-MM-DD.")]
    [SerializeField]
    private string versionDate;
    [Tooltip("The assistantId to run the example.")]
    [SerializeField]
    private string assistantId;
    #endregion

    private AssistantService service;

    private bool createSessionTested = false;
    private bool deleteSessionTested = false;
    private string sessionId;

    public ConversationResponseDelegate ConversationResponse = delegate { };

    public bool GetSessionService(){
      return createSessionTested;
    }

    private void Start()
    {
      LogSystem.InstallDefaultReactors();
      Runnable.Run(CreateService());
    }

    private IEnumerator CreateService()
    {
      if (string.IsNullOrEmpty(iamApikey))
      {
        throw new IBMException("Please provide IAM ApiKey for the service.");
      }

      //  Create credential and instantiate service
      IamAuthenticator authenticator = new IamAuthenticator(apikey: iamApikey);

      //  Wait for tokendata
      while (!authenticator.CanAuthenticate())
        yield return null;

      service = new AssistantService(versionDate, authenticator);
      if (!string.IsNullOrEmpty(serviceUrl))
      {
        service.SetServiceUrl(serviceUrl);
      }

      Log.Debug("Step 1:", "Creating Session...");
      service.CreateSession(OnCreateSession, assistantId);

      //  Wait for createSession
      while (!createSessionTested)
        yield return null;
    }

    private void OnMessageStart(DetailedResponse<MessageResponse> response, IBMError error)
    {
      if (error == null)
      {
        CaptureMessage(response);
      }
      else
      {
        Log.Debug("ExampleCallback.OnMessage()", "Error received: {0}, {1}, {3}", error.StatusCode, error.ErrorMessage, error.Response);
      }        
    }

    public void sendMessage(string message){
      if(string.IsNullOrEmpty(message)) service.Message(OnMessageStart, assistantId, sessionId);

      MessageInput messageRequest = new MessageInput()
      {
        Text = message
      };

      if(!service.Message(OnMessage, assistantId, sessionId, messageRequest)) 
        Debug.LogError("Failed to send the message to Conversation service!");
      
      Log.Debug("SendMessage:", "{0}", message);
    }

    private void OnMessage(DetailedResponse<MessageResponse> response, IBMError error)
    {
      if (error == null)
      {
        CaptureMessage(response);
      }
      else
      {
        Log.Debug("ExampleCallback.OnMessage()", "Error received: {0}, {1}, {2}", error.StatusCode, error.ErrorMessage, error.Response);
      }
    }

    private void CaptureMessage(DetailedResponse<MessageResponse> response){
      if (response is null)
      {
          throw new System.ArgumentNullException(nameof(response));
      }
      //  GET conteudo da messagens
      var messagesReceived = new List<string>(); 
      foreach (var message in response.Result.Output.Generic)
      {
        Log.Debug("", "response: {0}", message.Text); 
        messagesReceived.Add(message.Text);
      }

      //  GET entidades e intenção
      //  Entity -> Nome da Entidade
      //  Value -> Valor buscado na Entidade
      //  Intent -> Nome da Intenção
      string nameEntity = null;
      string valueEntity = null;
      string nameIntent = null;
      response.Result.Output.Entities.ForEach(value => {
        nameEntity = value.Entity;
        valueEntity = value.Value;
      });

      response.Result.Output.Intents.ForEach(value => {
        nameIntent = value.Intent;
      });

      //  Chama função retorno
      ConversationResponse(messagesReceived, nameEntity, valueEntity, nameIntent);
    }

    private void OnDeleteSession(DetailedResponse<object> response, IBMError error)
    {
      Log.Debug("ExampleAssistantV2.OnDeleteSession()", "Session deleted.");
      deleteSessionTested = true;
    }

    private void OnCreateSession(DetailedResponse<SessionResponse> response, IBMError error)
    {
      Log.Debug("ExampleAssistantV2.OnCreateSession()", "Session: {0}", response.Result.SessionId);
      sessionId = response.Result.SessionId;
      createSessionTested = true;
    }
  }    
}