using UnityEngine;
using UnityEngine.SceneManagement;

public class MorsecodeMachine : MonoBehaviour
{
    [HideInInspector]
    public static bool isMachineActive = false;

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

        if (!SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "soundHasPlayed", out bool hasPlayed))
        {
            FindObjectOfType<AudioManager>().PlaySound("Morsecodemachine activates");
        }
        SaveHandler.SaveLevel(this.name, "soundHasPlayed", true);

        if (morsecodeMachineActive)
        {
            morsecodeMachineDeactive.sprite = morsecodeMachineActive;
        }
    }
}
