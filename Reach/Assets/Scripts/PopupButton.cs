using System;
using UnityEditor;
using UnityEngine;

public class PopupButton : MonoBehaviour
{
    public PopupMenu PopupMenu;

    private void OnMouseDown()
    {
        PopupMenu.OpenPopupMenu();
    }
}
