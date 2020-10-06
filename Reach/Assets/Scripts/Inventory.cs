using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public static List<ItemSlot> ItemSlots;

    private void Awake()
    {
        ItemSlots = GetComponentsInChildren<ItemSlot>().ToList();

        List<Item> listItemsFromSaveFile = SaveHandler.GetSavedItemsForInventory();
        foreach (Item item in listItemsFromSaveFile)
        {
            AddItem(item);
        }
    }

    public static List<Item> GetCurrentItems()
    {
        List<Item> currentItems = new List<Item>();
        foreach (ItemSlot itemSlot in ItemSlots)
        {
            if (itemSlot.Item)
            {
                currentItems.Add(itemSlot.Item);
            }
        }

        return currentItems;
    }

    public static void AddItem(Item item)
    {
        int indexOfEmptyItemSlot = -1;
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            ItemSlot itemSlot = ItemSlots[i];
            if (itemSlot.Item == null)
            {
                indexOfEmptyItemSlot = i;
                break;
            }
        }

        if (indexOfEmptyItemSlot > -1)
        {
            //An empty item slot is found
            ItemSlot emptyItemSlot = ItemSlots[indexOfEmptyItemSlot];
            emptyItemSlot.AddItem(item);
        }
        else
        {
            //All item slots have items
        }
    }

    public static void RemoveItem(Item item)
    {
        IEnumerable<ItemSlot> itemSlotsWIthItemToRemove = ItemSlots.Where(i => i.Item == item);
        foreach (ItemSlot itemSlot in itemSlotsWIthItemToRemove)
        {
            itemSlot.RemoveItem();
        }
    }
}