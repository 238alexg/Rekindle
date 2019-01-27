using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFunctions : Button
{
    public AudioClip hover;
    public AudioClip click;


     protected override void Start()
    {
        base.Start();
        hover = GetComponent<AudioClip>();
        click = GetComponent<AudioClip>();
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.LogError("test");
        base.OnPointerEnter(eventData);
        AudioManager.PlayEffect(hover);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogError("test2");
        base.OnPointerClick(eventData);
        AudioManager.PlayEffect(click);
    }
}
