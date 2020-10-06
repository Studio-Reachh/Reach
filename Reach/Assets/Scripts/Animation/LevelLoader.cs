using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelLoader : MonoBehaviour
{
    [Header("Transition")]
    public Animator Crossfade;
    public VideoPlayer Cutscene;
    public GameObject VideoplayerCanvas;
    public float transitionTime = 1f;

    public static bool IsLoadingLevel = false;

    public void Awake()
    {
        IsLoadingLevel = false;
        if (VideoplayerCanvas)
        {
            VideoplayerCanvas.SetActive(false);
        }
    }
    public void LoadNextLevel(string sceneName, bool PlayCutscene, string Audio)
    {
        IsLoadingLevel = true;
        if (Cutscene && PlayCutscene)
        {
            StartCoroutine(PlayCutSceneBeforeLoadingLevel(sceneName));
        }
        else
        {
            StartCoroutine(LoadLevel(sceneName));
            if (Audio != string.Empty)
            {
                FindObjectOfType<AudioManager>().PlaySound(Audio);
            }
        }
    }

    IEnumerator LoadLevel(string level)
    {
        //Trigger scene transition
        Crossfade.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(level);
    }

    IEnumerator PlayCutSceneBeforeLoadingLevel(string level)
    {
        VideoplayerCanvas.SetActive(true);
        Cutscene.Play();

        yield return new WaitForSeconds(Convert.ToSingle(Cutscene.clip.length));

        //Load Scene
        SceneManager.LoadScene(level);
    }
}
