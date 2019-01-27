using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneLoader : MonoBehaviour
{
    public AudioSource AudioPlayer;
    private static Canvas StartCanvas;
    private static Canvas DeveloperCanvas;
    private bool clicked;

    private void Start()
    {
        StartCanvas = GetComponent<Canvas>();
        StartCanvas.enabled = true;
        DeveloperCanvas = GetComponent<Canvas>();
        DeveloperCanvas.enabled = true;
    }

    public void DisableCanvas()
    {
         StartCanvas.enabled = false;
    }

    public void EnableCanvas()
    {
        DeveloperCanvas.enabled = true;
    }
}
