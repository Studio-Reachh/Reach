using UnityEngine;
using System.Collections;

public abstract class PopupMenu : MonoBehaviour
{
    public GameObject popupMenuUI;
    public static bool isPopupOpen;
    private GameObject _inventory;
    public virtual void Awake()
    {
        popupMenuUI.SetActive(false);
        _inventory = GameObject.Find("Inventory[canvas]");
    }
    public virtual void OpenPopupMenu()
    {
        popupMenuUI.SetActive(true);
        isPopupOpen = true;
        _inventory.SetActive(false);
    }
    public virtual void ClosePopupMenu()
    {
        popupMenuUI.SetActive(false);
        isPopupOpen = false;
        _inventory.SetActive(true);
    }
}
