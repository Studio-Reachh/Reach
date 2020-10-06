using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MorsecodeMachinePopup : PopupMenu
{
    //Change UI Sprites
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

    private bool _morsecodeSolved;

    private IEnumerator WriteMessageCoroutine;

    public void Start()
    {
        WriteMessageCoroutine = WritingMessage(UITextReceive, MorsecodeReceivedMessage, TypeSpeed, MorsecodeReceivedPauseDuration, 0);

        if (SaveHandler.GetValueByProperty("Room02", this.name, "MorsecodeSend", out bool morsecodeSolved))
        {
            _morsecodeSolved = morsecodeSolved;
        }
    }

    public override void ClosePopupMenu()
    {
        base.ClosePopupMenu();

        //Stop writing message when popup is closed
        StopCoroutine(WriteMessageCoroutine);
    }

    public override void OpenPopupMenu()
    {
        base.OpenPopupMenu();

        if (isPopupOpen && MorsecodeMachine.isMachineActive)
        {
            ActivateScreens();
        }
    }

    public void ActivateScreens()
    {
        DeactiveScreenImage.sprite = ActiveScreenSprite;

        if (!_morsecodeSolved)
        {
            if (!SaveHandler.GetValueByProperty("Room02", this.name, "MessageReceived", out bool messageReceived))
            {
                StartCoroutine(WriteMessageCoroutine);
            } else
            {
                UITextReceive.text = MorsecodeReceivedMessage;
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

        if (!MorsecodeMachine.isMachineActive)
        {
            return;
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
            FindObjectOfType<AudioManager>().PlaySound("Morsecode message");

            while (characterIndex < TextToWrite.Length)
            {
                //write message letter by letter
                characterIndex++;
                UIText.text = TextToWrite.Substring(0, characterIndex);

                yield return new WaitForSeconds(TypeSpeed);
            }

            SaveHandler.SaveLevel(this.name, "MessageReceived", true);

            yield return new WaitForSeconds(messagePause);
        }
    }
}
