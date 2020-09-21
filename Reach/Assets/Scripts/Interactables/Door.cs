using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    private LevelLoader _levelLoader;

    [Header("Load Next Level")]
    public string LevelName;

    [Header("Unlock Door")]
    public bool IsDoorLocked;
    public string KeyGameobject;
    public string KeyProperty;

    [Header("Play Cutscene")]
    public bool PlayCutscene;

    private void Awake()
    {
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    public override bool Interact(Item item)
    {
        return true;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (IsDoorLocked)
            {
                if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, KeyGameobject, KeyProperty, out bool isUnlocked))
                {
                    if (LevelName != string.Empty)
                    {
                        _levelLoader.LoadNextLevel(LevelName, PlayCutscene);
                        Interact(null);
                    }
                }
            }
            else
            {
                if (LevelName != string.Empty)
                {
                    _levelLoader.LoadNextLevel(LevelName, PlayCutscene);
                    Interact(null);
                }
            }
        }
    }
}
