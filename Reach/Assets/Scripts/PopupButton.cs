using System;
using UnityEditor;
using UnityEngine;

public class PopupButton : MonoBehaviour
{
    public PopupMenu _popupMenu;

    private void OnMouseDown()
    {
        _popupMenu.OpenPopupMenu();
    }
}
