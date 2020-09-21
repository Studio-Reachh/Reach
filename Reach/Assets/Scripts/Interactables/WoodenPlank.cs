using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WoodenPlank : Interactable
{
    private Item _itemToPickup;

    private void Awake()
    {
        _itemToPickup = Resources.Load<Item>("ScriptableObjects/WoodenPlank");
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "PickedUp", out bool isPickedUp))
        {
            if (isPickedUp)
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

            SaveHandler.SaveLevel(name, "PickedUp", true);
            Destroy(this.gameObject);
        }

        return successfulInteraction;
    }
}
