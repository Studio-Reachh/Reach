using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteMessage : MonoBehaviour
{
    public bool messageFinished;
    public IEnumerator WritingMessage(Text UIText, string TextToWrite, float TypeSpeed, float messagePause, int characterIndex)
    {
        messageFinished = false;

        if (UIText)
        {
            while (characterIndex < TextToWrite.Length)
            {
                //write message letter by letter
                characterIndex++;
                UIText.text = TextToWrite.Substring(0, characterIndex);

                yield return new WaitForSeconds(TypeSpeed);
            }

            yield return new WaitForSeconds(messagePause);

            messageFinished = true;
        }
    }
}
