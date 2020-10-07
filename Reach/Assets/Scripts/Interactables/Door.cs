using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    private LevelLoader _levelLoader;
    private bool _isUnlocked;

    [Header("Load Next Level")]
    public string LevelName;

    [Header("Unlock Door")]
    public bool IsDoorLocked;
    public string KeyGameobject;
    public string KeyProperty;

    [Header("Play Cutscene")]
    public bool PlayCutscene;

    [Header("Audio")]
    public string Audio;

    private void Awake()
    {
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    private void Update()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, KeyGameobject, KeyProperty, out bool isUnlocked))
        {
            _isUnlocked = isUnlocked;
        }

        if (IsDoorLocked )
        {
            if (_isUnlocked && !SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, this.name, "SoundHasPlayed", out bool SoundHasPlayed))
            {
                FindObjectOfType<AudioManager>().PlaySound("Unlocked");

                SaveHandler.SaveLevel(this.name, "SoundHasPlayed", true);
            }
        }
    }

    public override bool Interact(Item item)
    {
        bool succesfullInteraction = false;

        if (item)
        {
            return false;
        }

        if (IsDoorLocked)
        {
            if (_isUnlocked)
            {
                if (LevelName != string.Empty)
                {
                    _levelLoader.LoadNextLevel(LevelName, PlayCutscene, Audio);
                    succesfullInteraction = true;
                } 
            }
            else
            {
                FindObjectOfType<AudioManager>().PlaySound("Locked");
            }
        }
        else
        {
            if (LevelName != string.Empty)
            {
                _levelLoader.LoadNextLevel(LevelName, PlayCutscene, Audio);
                succesfullInteraction = true;
            }
        }

        return succesfullInteraction;
    }
}
