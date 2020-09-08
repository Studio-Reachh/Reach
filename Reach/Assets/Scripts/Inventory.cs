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
    }

    private void Start()
    {
        AddItem(Resources.Load<Item>("ScriptableObjects/TestItem"));
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
}