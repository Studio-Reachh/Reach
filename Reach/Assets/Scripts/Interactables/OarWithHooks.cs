using UnityEngine;
using UnityEngine.SceneManagement;

public class OarWithHooks : Interactable
{
    private Item _itemToPickup;

    private void Awake()
    {
        _itemToPickup = Resources.Load<Item>("ScriptableObjects/OarWithHooks");

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
