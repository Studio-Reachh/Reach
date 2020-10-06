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
        if (!isMachineActive)
        {
            //check if previous puzzels have been solved before unlocking machine | ceiling has a typo
            if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "FixedPipe", "HasPipe", out bool hasPipe) &&
                SaveHandler.GetValueByProperty("Room01", "CeillingHole (Empty GO)", "HasPlank", out bool hasPlank))
            {
                ActivateMachine();
            }
        }
    }

    public void ActivateMachine()
    {
        isMachineActive = true;

        FindObjectOfType<AudioManager>().PlaySound("Morsecodemachine activates");

        if(morsecodeMachineActive)
        {
            morsecodeMachineDeactive.sprite = morsecodeMachineActive;
        }
    }
}
