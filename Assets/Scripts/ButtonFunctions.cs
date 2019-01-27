using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFunctions : Button
{
    private AudioSource audioPlayer;
    public AudioClip hover;
   


    protected override void Start()
    {
        base.Start();
        audioPlayer = GetComponent<AudioSource>();
  
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        audioPlayer.PlayOneShot(hover);
    }
  /*  public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        audioPlayer.PlayOneShot();
    }*/
}
