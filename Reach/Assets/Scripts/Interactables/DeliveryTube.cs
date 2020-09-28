using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliveryTube : Interactable
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private bool _hasItem = false;
    private bool _isPipeDelivered = false;

    [Header("Item")]
    public string ItemSaveProperty;
    public Item ItemNeeded;

    [Header("Popup")]
    public PopupMenu PopupMenu;

    [Header("Change Sprite")]
    public bool changeSprite;
    public Sprite sprite;

    private void Update()
    {
        if (!_isPipeDelivered)
        {
            if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "Pipe[image]", "HasBeenDelivered", out bool hasItem))
            {
                _isPipeDelivered = true;
                _spriteRenderer.enabled = false;
            }
        }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

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

        if(!item)
        {
            //open popup
            PopupMenu.OpenPopupMenu();
        } else
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
        }

        return succesfulInteraction;
    }
}
