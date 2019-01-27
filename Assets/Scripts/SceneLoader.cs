using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public AudioSource AudioPlayer;
    
    public void LoadScene(int level)
    {
        while(AudioPlayer.isPlaying)
        {
            ;
        }
        UnityEngine.Application.LoadLevel(level);
    }
}
