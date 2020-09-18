using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private LevelLoader _levelLoader;
    public string LevelName;

    private void Awake()
    {
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    public override bool Interact(Item item)
    {
        if (item == null && !string.IsNullOrEmpty(LevelName))
        {
            _levelLoader.LoadNextLevel(LevelName);
            return true;
        }

        return false;
    }
}
