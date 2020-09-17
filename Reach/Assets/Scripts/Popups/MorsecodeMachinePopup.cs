using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MorsecodeMachinePopup : PopupMenu
{
    //Change UI Sprites
    [Header("Morsecodemachine")]
    public MorsecodeMachine MorsecodeMachine;

    [Header("ShakeCamera")]
    public CameraShake CameraShake;
    public float MagnitudeShake;

    [Header("Sprites")]
    public Sprite ActiveScreenSprite;
    public Image DeactiveScreenImage;
    public Sprite KeyerUpSprite;
    public Sprite KeyerDownSprite;
    public Image KeyerImage;

    [Header("MorsecodeMessage")]
    public float TypeSpeed;
    public string MorsecodeReceivedMessage;
    /// <summary>
    /// MorsecodeReceivedPauseDuration -> pauze duration after writing message and before initiating next method.
    /// </summary>
    public float MorsecodeReceivedPauseDuration;
    public string MorsecodeSendMessage;
    public float MorsecodeSendPauseDuration;
    public float DotDuration;
    public float DashDuration;

    [Header("Text")]
    public Text UITextReceive;
    public Text UITextSend;
    public string EndMessage;

    private bool _morsecodeSolved, _messageReceived = false;

    public void Start()
    {
        if (SaveHandler.GetValueByProperty("Room02", "MorsecodeMachine[canvas]", "MorsecodeSend", out bool morsecodeSolved))
        {
            _morsecodeSolved = morsecodeSolved;
        }
    }

    public override void OpenPopupMenu()
    {
        base.OpenPopupMenu();

        if(isPopupOpen && MorsecodeMachine.isMachineActive)
        {
            ActivateScreens();
        }
    }

    public void ActivateScreens()
    {
        DeactiveScreenImage.sprite = ActiveScreenSprite;

        if (!_morsecodeSolved)
        {
            if (!_messageReceived)
            {
                StartCoroutine(WritingMessage(UITextReceive, MorsecodeReceivedMessage, TypeSpeed, MorsecodeReceivedPauseDuration, 0));
            } else
            {
                StartCoroutine(PlayMorsecodeMessage());
            }
        }

        if (_morsecodeSolved)
        {
            if (UITextSend && EndMessage != string.Empty)
            {
                UITextReceive.text = MorsecodeReceivedMessage;
                UITextSend.text = EndMessage;
            }
        }
    }

    public IEnumerator PlayMorsecodeMessage()
    {
        while (!_morsecodeSolved)
        {
            foreach (var letter in MorsecodeSendMessage)
            {
                switch (letter)
                {
                    case '.':
                        Vibration.CreateOneShot(500);
                        StartCoroutine(CameraShake.Shake(DotDuration, MagnitudeShake));
                        yield return new WaitForSeconds(1.50f);
                        break;

                    case '-':
                        Vibration.CreateOneShot(2500);
                        StartCoroutine(CameraShake.Shake(DashDuration, MagnitudeShake));
                        yield return new WaitForSeconds(3.50f);
                        break;

                    case ' ':
                        yield return new WaitForSeconds(1);
                        break;
                }
            }
            yield return new WaitForSeconds(10);
        }
    }

    public void KeyerInput()
    {
        //onmouse button down
        if (KeyerDownSprite)
        {
            KeyerImage.sprite = KeyerDownSprite;
        }
    }

    public void KeyerOutput(float holdDownTime)
    {
        if(KeyerUpSprite)
        {
            KeyerImage.sprite = KeyerUpSprite;
        }

        if (!_morsecodeSolved)
        {
            if (holdDownTime < DotDuration && UITextSend)
            {
                //write dot
                UITextSend.text = UITextSend.text + ".";

            }
            else if (holdDownTime > DotDuration && holdDownTime < DashDuration && UITextSend)
            {
                //write dash
                UITextSend.text = UITextSend.text + "-";
            }
            else
            {
                //TO-DO: give feedback is wrong
            }
        }

        //If string reached max length check message
        if (UITextSend.text.Length == MorsecodeSendMessage.Length)
        {
            StartCoroutine(CheckMorseMessage());
        }
    }

    public IEnumerator CheckMorseMessage()
    {
        yield return new WaitForSeconds(1);

        if (UITextSend.text == MorsecodeSendMessage)
        {
            StartCoroutine(WritingMessage(UITextSend, EndMessage, TypeSpeed, MorsecodeSendPauseDuration, 0));
            UITextSend.text = EndMessage;

            _morsecodeSolved = true;
            StopCoroutine(PlayMorsecodeMessage());

            //safe game progress
            SaveHandler.SaveLevel(this.name, "MorsecodeSend", true);
        } else
        {
            //reset text
            UITextSend.text = "";
        }
    }

    public IEnumerator WritingMessage(Text UIText, string TextToWrite, float TypeSpeed, float messagePause, int characterIndex)
    {
        if (UIText)
        {
            while (characterIndex < TextToWrite.Length)
            {
                //write message letter by letter
                characterIndex++;
                UIText.text = TextToWrite.Substring(0, characterIndex);

                yield return new WaitForSeconds(TypeSpeed);
            }

            _messageReceived = true;
            yield return new WaitForSeconds(messagePause);

            StartCoroutine(PlayMorsecodeMessage());
        }
    }
}
