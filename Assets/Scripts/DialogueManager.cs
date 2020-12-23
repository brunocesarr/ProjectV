using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WatsonAssistant;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    ///	Dialogue
    private Dialogue Dialogue;

    public GameObject input;
    private TouchScreenKeyboard keyboard;

    private WatsonConversation serviceWatson;
    private TextToSpeech serviceTextToSpeech;
    private PathManager servicePathManager;
    private PlayerControllerAnimation servicePlayerAnimation;

    private GameObject controller;

    private float time;

    private void Awake()
    {
        serviceWatson = gameObject.GetComponent<WatsonConversation>();
        serviceTextToSpeech = gameObject.GetComponent<TextToSpeech>();
        servicePathManager = GameObject.Find("AIPath").GetComponent<PathManager>();
        servicePlayerAnimation = gameObject.GetComponent<PlayerControllerAnimation>();
    }

    // Use this for initialization
    void Start()
    {
        time = 0.0f;
        Setup();
    }

    void Setup()
    {
        input.SetActive(false);

        serviceWatson.ConversationResponse += OnConversationResponse;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= 15.0f && (servicePlayerAnimation.ValidarEstaOuvindo()))
        {
            servicePlayerAnimation.StartDance();
            time = 0.0f;
        }

        if (TouchScreenKeyboard.visible)
        {
            input.transform.position = new Vector3(Screen.width / 2, Screen.height - 100, input.transform.position.z);
        }
        else
        {
            input.transform.position = new Vector3(Screen.width / 2, 100, input.transform.position.z);
        }
    }

    public void StartDialogue()
    {
        Dialogue = new Dialogue();

        while (!serviceTextToSpeech.GetSessionService() || !serviceWatson.GetSessionService())
        {
            Debug.Log("Serviços inicializando!");
        }

        input.SetActive(true);
        Dialogue.Sentences.Clear();
        EventSystem.current.SetSelectedGameObject(input.gameObject, null);
    }

    public void DisplayNextSentence()
    {
        while (Dialogue.Sentences.Count > 0)
        {
            servicePlayerAnimation.StartTalking();
            string sentence = Dialogue.Sentences.Dequeue();
            StopAllCoroutines();
            serviceTextToSpeech.RunTextToSpeech(sentence);
            servicePlayerAnimation.StopTalking();
        }

        input.gameObject.SetActive(true);
    }

    public void SendMessage()
    {
        string message = GameObject.Find("InputField").GetComponent<InputField>().text;

        if (!string.IsNullOrEmpty(message))
        {
            try
            {
                this.serviceWatson.sendMessage(message);
                GameObject.Find("InputField").GetComponent<InputField>().text = null;

                input.gameObject.SetActive(false);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
                throw;
            }
        }
    }

    private void OnConversationResponse(List<string> messages, string nameEntity, string valueEntity, string nameIntent)
    {
        if (messages == null || messages.Count == 0)
            return;

        if (ChatDialogValidation(nameEntity, nameIntent))
        {
            SearchDestiny(valueEntity, nameEntity);
            return;
        }

        messages.ForEach(message =>
        {
            Dialogue.Sentences.Enqueue(message);
        });
        DisplayNextSentence();

        if (nameIntent == "General_Ending")
        {
            FinalizeDialogue();
        }
    }

    private bool ChatDialogValidation(string nameEntity, string nameIntent)
    {
        return nameEntity != null
            && nameIntent != null
            && (nameEntity == "teacher" || nameEntity == "laboratory" ||
                 nameEntity == "researchGroup" || nameEntity == "anotherPlace")
            && nameIntent == "Help_Route";
    }

    private void SearchDestiny(string valueEntity, string nameEntity)
    {
        if (nameEntity.Equals("laboratory") || nameEntity.Equals("teacher") ||
            nameEntity.Equals("searchGroup") || nameEntity.Equals("anotherPlace"))
        {
            FinalizeDialogue();
            servicePathManager.ToggleCamera();
            servicePathManager.SetDestinyPoint(valueEntity);
        }
        else
        {
            Dialogue.Sentences.Enqueue("Não entendi!");
            DisplayNextSentence();
        }
    }

    private void FinalizeDialogue()
    {
        input.SetActive(false);
    }

}
