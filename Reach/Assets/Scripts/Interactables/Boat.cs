using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : Interactable
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private LevelLoader _levelLoader;
    private bool _hasItem = false;

    [Header("Item")]
    public string ItemSaveProperty;
    public Item ItemNeeded;
    public string LevelName;

    [Header("Change Sprite")]
    public bool changeSprite;
    public Sprite sprite;

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

        if (!_hasItem && item && item == ItemNeeded)
        {
            succesfulInteraction = true;
            _hasItem = true;

            AddItem();
            SaveHandler.SaveLevel(this.name, ItemSaveProperty, true);

            if (LevelName != string.Empty)
            {
                _levelLoader.LoadNextLevel(LevelName, true, Audio);
            }
        }

        return succesfulInteraction;
    }
}
