using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public string GetNextScene;
    public float transitionTime = 1f;

    private void OnMouseDown()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        if(GetNextScene != string.Empty)
        {
            StartCoroutine(LoadLevel(GetNextScene));
        }
    }

    IEnumerator LoadLevel(string level)
    {
        //Trigger scene transition
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(level);
    }
}
