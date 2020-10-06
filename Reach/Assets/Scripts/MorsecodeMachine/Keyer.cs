﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Keyer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private MorsecodeMachinePopup _morsecodeMachinePopup;
    private float holdDownStartTime;

    public void Awake()
    {
        _morsecodeMachinePopup = transform.parent.parent.gameObject.GetComponent<MorsecodeMachinePopup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _morsecodeMachinePopup.KeyerInput();
        holdDownStartTime = Time.time;

        if (MorsecodeMachine.isMachineActive)
        {
            FindObjectOfType<AudioManager>().PlaySound("Morsecode sound");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (MorsecodeMachine.isMachineActive)
        {
            FindObjectOfType<AudioManager>().StopSound("Morsecode sound");
        }

        float holdDownTime = Time.time - holdDownStartTime;
        _morsecodeMachinePopup.KeyerOutput(holdDownTime);
    }
}
