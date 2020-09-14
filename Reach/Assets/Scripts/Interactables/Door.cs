using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private LevelLoader _levelLoader;
    public string LevelName;

    public override bool Interact(Item item)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    private void OnMouseDown()
    {
        if(LevelName != string.Empty)
        {
            _levelLoader.LoadNextLevel(LevelName);
            Interact(null);
        }
    }
}
