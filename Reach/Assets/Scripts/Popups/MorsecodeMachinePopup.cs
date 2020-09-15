using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MorsecodeMachinePopup : PopupMenu
{
    //Adjust AI Elementen
    [Header("MorsecodeMessage")]
    public Text morsecodeText;
    public string morsecodeMessage;
    public float typeSpeed;

    [Header("MorsecodeInteractable")]
    public MorsecodeMachine morsecodeMachine;

    public void Start()
    {
        morsecodeText = transform.Find("PopupScreen").Find("InitialMessage").GetComponent<Text>();
    }

    public override void OpenPopupMenu()
    {
        base.OpenPopupMenu();

        if(isPopupOpen && morsecodeMachine.machineActive && !morsecodeMachine.messageHasPlayed)
        {
            SendMessage();

        } else if(isPopupOpen && morsecodeMachine.machineActive && morsecodeMachine.messageHasPlayed)
        {
            morsecodeText.text = morsecodeMessage;
            StartCoroutine(morsecodeMachine.PlayMorsecode(morsecodeMachine.morsecodeMessage));
        }
    }

    public void EnableKeyer()
    {
        if(morsecodeMachine.machineActive)
        {
            morsecodeMachine.UseKeyer();
        }
    }

    public void SendMessage()
    {
        StartCoroutine(morsecodeMachine.WriteMessage(morsecodeText, morsecodeMessage, typeSpeed));
    }
}
