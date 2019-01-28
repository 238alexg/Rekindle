using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    private static Canvas UICanvas;

    private void Start()
    {
        UICanvas = GetComponent<Canvas>();
        UICanvas.enabled = true;
    }

    public void DisableCanvas()
    {
        UICanvas.enabled = false;

    }

    public void EnableCanvas()
    {
        UICanvas.enabled = true;
    }
}
