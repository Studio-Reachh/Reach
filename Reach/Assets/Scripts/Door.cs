using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private LevelLoader _levelLoader;
    public string LevelName;

    private void Awake()
    {
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    private void OnMouseDown()
    {
        if(LevelName != string.Empty)
        {
            _levelLoader.LoadNextLevel(LevelName);
        }
    }
}
