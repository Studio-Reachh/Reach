using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public Sprite Image;

    /// <summary>
    /// Destroy the item when it was succesfully used when interacting with an interactable
    /// </summary>
    public bool DestroyOnSuccessfulUse;
}
