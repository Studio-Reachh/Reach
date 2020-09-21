using UnityEngine;
using UnityEngine.SceneManagement;

public class DroppedItem : Interactable
{
    [Header("Item to pick up")]
    public Item Item;
    public string ItemProperty;

    private void Awake()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, ItemProperty, out bool isPickedup))
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

        if (Item)
        {
            Inventory.AddItem(Item);
            successfulInteraction = true;

            SaveHandler.SaveLevel(name, ItemProperty, true);
            Destroy(this.gameObject);
        }

        return successfulInteraction;
    }
}
