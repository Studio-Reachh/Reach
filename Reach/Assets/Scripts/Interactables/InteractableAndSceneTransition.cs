using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableAndSceneTransition : Interactable
{
    private SpriteRenderer _spriteRenderer;
    private LevelLoader _levelLoader;
    private bool _hasItem = false;
    private bool _isUnlocked;

    [Header("Item")]
    public string ItemSaveProperty;
    public Item ItemNeeded;

    [Header("Change Sprite")]
    public bool changeSprite;
    public Sprite sprite;

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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, ItemSaveProperty, out bool hasItem))
        {
            _hasItem = hasItem;
            AddItem();
        }
    }

    private void Update()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, KeyGameobject, KeyProperty, out bool isUnlocked))
        {
            _isUnlocked = isUnlocked;
        }

        if (IsDoorLocked)
        {
            if (_isUnlocked && !SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, this.name, "SoundHasPlayed", out bool SoundHasPlayed))
            {
                FindObjectOfType<AudioManager>().PlaySound("Unlocked");

                SaveHandler.SaveLevel(this.name, "SoundHasPlayed", true);
            }
        }
    }

    private void AddItem()
    {
        if (changeSprite)
        {
            _spriteRenderer.sprite = sprite;
        }
        else
        {
            _spriteRenderer.enabled = true;
        }
    }

    public override bool Interact(Item item)
    {
        bool succesfulInteraction = false;

        if (item)
        {
            if (!_hasItem && item && item == ItemNeeded)
            {
                succesfulInteraction = true;
                _hasItem = true;

                FindObjectOfType<AudioManager>().PlaySound("Item placed right");

                AddItem();
                SaveHandler.SaveLevel(this.name, ItemSaveProperty, true);
            }
            else
            {
                FindObjectOfType<AudioManager>().PlaySound("Item placed wrong");
            }
        } else
        {
            if (IsDoorLocked)
            {
                if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, KeyGameobject, KeyProperty, out bool isUnlocked))
                {
                    if (LevelName != string.Empty)
                    {
                        _levelLoader.LoadNextLevel(LevelName, PlayCutscene, Audio);
                        succesfulInteraction = true;
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
                    succesfulInteraction = true;
                }
            }
        }

        return succesfulInteraction;
    }
}
