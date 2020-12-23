using IBM.Watson.TextToSpeech.V1;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.Authentication.Iam;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using IBM.Cloud.SDK;
using System.Threading.Tasks;

public class TextToSpeech : MonoBehaviour
{
    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [Space(10)]
    [Tooltip("The IAM apikey.")]
    [SerializeField]
    private string iamApikey;
    [Tooltip("The service URL (optional). This defaults to \"https://gateway.watsonplatform.net/text-to-speech/api\"")]
    [SerializeField]
    private string serviceUrl;
    private TextToSpeechService service;
    private string allisionVoice = "pt-BR_IsabelaV3Voice";
    private string synthesizeMimeType = "audio/wav";
    private bool _textEntered = false;
    private AudioClip _recording = null;
    private byte[] audioStream = null;
    private bool createSessionTested = false;
    #endregion

    public bool GetSessionService(){
        return createSessionTested;
    }

    private void Start()
    {
        LogSystem.InstallDefaultReactors();
        Runnable.Run(CreateService());
    }

    public IEnumerator RunTaskTextToSpeech(string texto)
    {
        Runnable.Run(Synthesize(texto));
        return null;
    }
    public int RunTextToSpeech(string texto)
    {
        return Runnable.Run(Synthesize(texto));
    }

    private IEnumerator CreateService()
    {
        if (string.IsNullOrEmpty(iamApikey))
        {
            throw new IBMException("Please add IAM ApiKey to the Iam Apikey field in the inspector.");
        }

        IamAuthenticator authenticator = new IamAuthenticator(apikey: iamApikey);

        while (!authenticator.CanAuthenticate())
        {
            yield return null;
        }

        service = new TextToSpeechService(authenticator);
        if (!string.IsNullOrEmpty(serviceUrl))
        {
            service.SetServiceUrl(serviceUrl);
            createSessionTested = true;
        }
    }

    #region Synthesize Example
    public IEnumerator Synthesize(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            text = "";
            Log.Debug("ExampleTextToSpeechV1", "Using default text, please enter your own text in dialog box!");
        }

        byte[] synthesizeResponse = null;
        AudioClip clip = null;
        service.Synthesize(
            callback: (DetailedResponse<byte[]> response, IBMError error) =>
            {
                synthesizeResponse = response.Result;
                Log.Debug("ExampleTextToSpeechV1", "Synthesize done!");
                clip = WaveFile.ParseWAV("myClip", synthesizeResponse);
                PlayClip(clip);
            },
            text: text,
            voice: allisionVoice,
            accept: synthesizeMimeType
        );

        while (synthesizeResponse == null)
            yield return null;

        yield return new WaitForSeconds(clip.length);
    }
    #endregion

    #region PlayClip
    private void PlayClip(AudioClip clip)
    {
        if (Application.isPlaying && clip != null)
        {
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            source.Play();

            GameObject.Destroy(audioObject, clip.length);
        }
    }
    #endregion
}
