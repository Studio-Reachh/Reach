using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    private LevelLoader _levelLoader;

    private void Awake()
    {
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        _levelLoader.LoadNextLevel("Room01", true);
    }

    public void ContinueGame()
    {
        _levelLoader.LoadNextLevel("Room01", true);
    }
}
