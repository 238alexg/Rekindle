using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneLoader : MonoBehaviour
{
    public AudioSource AudioPlayer;
    private static Canvas Canvas;

    private void Start()
    {
        Canvas = GetComponent<Canvas>();
        Canvas.enabled = true;
    }

    public void DisableCanvas()
    {
        Canvas.enabled = false;

    }

    public void EnableCanvas()
    {
        Canvas.enabled = true;
    }
}