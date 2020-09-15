using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MorsecodeMachine : MonoBehaviour
{
    private AudioSource _currentSoundSource;

    [HideInInspector] public bool machineActive;
    [HideInInspector] public bool messageHasPlayed = false;

    private int characterIndex = 0;

    [Header("Audio")]
    public AudioClip morseKeyer;
    public AudioClip iniateMessage;
    public AudioClip morseMessage;

    [Header("MorsecodeMessage")]
    public string morsecodeMessage;

    [Header("Sprites")]
    public Sprite morsecodeMachineActive;
    public Sprite morsecodeScreenActive;
    public SpriteRenderer backgroundSprite;
    public Image morsecodeScreen;

    [Header("Popup")]
    public MorsecodeMachinePopup morsecodePopup;

    private void Awake()
    {
        _currentSoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
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
        machineActive = true;

        if (morsecodeScreen != null && morsecodeScreenActive != null)
        {
            //change screen sprite
            morsecodeScreen.sprite = morsecodeScreenActive;
        }

        if (morsecodeMachineActive != null)
        {
            //change background sprite
            backgroundSprite.sprite = morsecodeMachineActive;
        }
    }

    public IEnumerator WriteMessage(Text uiText, string textToWrite, float timePerCharacter)
    {
        if (uiText != null)
        {
            if (iniateMessage)
            {
                _currentSoundSource.PlayOneShot(iniateMessage, 1);
            }
            while (characterIndex < textToWrite.Length)
            {
                //write message
                characterIndex++;
                uiText.text = textToWrite.Substring(0, characterIndex);
              
                yield return new WaitForSeconds(timePerCharacter);
            }

            if(characterIndex == textToWrite.Length)
            {
                messageHasPlayed = true;
            }

            yield return new WaitForSeconds(5);

            StartCoroutine(PlayMorsecode(morsecodeMessage));
        }
    }

    public void UseKeyer()
    {
    }

    public IEnumerator PlayMorsecode(string message)
    {
        //while puzzle not is solved

        foreach (var letter in message)
        {
            switch (letter)
            {
                case '.' :
                    Vibration.CreateOneShot(1500);
                    yield return new WaitForSeconds(3);
                    break;

                case '-':
                    Vibration.CreateOneShot(5000);
                    yield return new WaitForSeconds(6);
                    break;

                case ' ':
                    yield return new WaitForSeconds(3);
                    break;
            }
        }
    }
}
