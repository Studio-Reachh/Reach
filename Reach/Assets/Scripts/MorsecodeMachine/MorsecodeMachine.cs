using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MorsecodeMachine : MonoBehaviour
{
    [HideInInspector]
    public bool isMachineActive = false;

    [Header("Sprites")]
    public Sprite morsecodeMachineActive;
    public SpriteRenderer morsecodeMachineDeactive;

    public void Update()
    {
        //check if previous puzzels have been solved before unlocking machine | ceiling has a typo
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "FixedPipe", "HasPipe", out bool hasPipe) &&
            SaveHandler.GetValueByProperty("Room01", "CeilingHole", "HasPlank", out bool hasPlank))
        {
            ActivateMachine();
        }
    }

    public void ActivateMachine()
    {
        isMachineActive = true;

        if(morsecodeMachineActive)
        {
            morsecodeMachineDeactive.sprite = morsecodeMachineActive;
        }
    }
}
