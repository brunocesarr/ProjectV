using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue();
        GameObject.Find("StartButton").SetActive(false);
    }
}
