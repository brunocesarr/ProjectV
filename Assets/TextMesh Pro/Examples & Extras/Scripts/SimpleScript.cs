using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    public class SimpleScript : MonoBehaviour
    {
        GameObject canvasNewDestination;
        GameObject SceneController;
        void Start()
        {
            canvasNewDestination = GameObject.Find("Canvas-NewDestination");
            SceneController = GameObject.Find("Controller");

            canvasNewDestination.SetActive(false);
        }

        void Update()
        {
        }

    }
}
