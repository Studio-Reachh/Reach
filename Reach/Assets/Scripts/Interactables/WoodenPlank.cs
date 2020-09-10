using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenPlank : Interactable
{
    private Item _itemToPickup;

    private void Awake()
    {
        _itemToPickup = Resources.Load<Item>("ScriptableObjects/WoodenPlank");
    }

    public override bool Interact(Item item)
    {
        bool successfulInteraction = false;

        if (_itemToPickup)
        {
            Inventory.AddItem(_itemToPickup);
            successfulInteraction = true;
        }

        return successfulInteraction;
    }

    private void OnMouseDown()
    {
        if (Interact(null))
        {
            Destroy(this.gameObject);
        }
    }
}
