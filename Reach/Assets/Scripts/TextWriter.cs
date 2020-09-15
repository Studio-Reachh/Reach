using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private Text uiText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }

    public IEnumerator WriteMessage(Text uiText, string textToWrite, float timePerCharacter)
    {
        if (uiText != null)
        {
            while (characterIndex <= textToWrite.Length)
            {
                characterIndex++;
                uiText.text = textToWrite.Substring(0, characterIndex);
                print("write" + characterIndex);
                //write message

                yield return new WaitForSeconds(1);
            }
        }

    }

    //private void Awake()
    //{
    //    StartCoroutine(WriteText());
    //}
    //private IEnumerator WriteText()
    //{
    //while (string nog niet klaar is)
    //{
    //uiText.text += "a";
    //yield return new WaitForSeconds(1f);
    //}
    //}

    private void Update()
    {
        if(uiText != null)
        {
            timer -= Time.deltaTime;
            while(timer <= 0f)
            {
                //Display next character
                timer += timePerCharacter;
                characterIndex++;
                uiText.text = textToWrite.Substring(0, characterIndex);

                if(characterIndex >= textToWrite.Length)
                {
                    //Entire string displayed
                    uiText = null;
                    return;
                }
            }
        }
    }
}
