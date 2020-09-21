using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DroppedItem : Interactable
{
    private Item _itemToPickup;

    [Header("Item to pick up")]
    public string ItemName;
    public string ItemProperty;

    private void Awake()
    {
        _itemToPickup = Resources.Load<Item>("ScriptableObjects/" + ItemName);

        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, ItemName, ItemProperty, out bool isPickedup))
        {
            if (isPickedup)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public override bool Interact(Item item)
    {
        bool successfulInteraction = false;

        if (_itemToPickup)
        {
            Inventory.AddItem(_itemToPickup);
            successfulInteraction = true;

            SaveHandler.SaveLevel(ItemName, ItemProperty, true);
        }

        return successfulInteraction;
    }
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Interact(null))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
