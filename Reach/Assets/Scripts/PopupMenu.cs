using UnityEngine;
using System.Collections;

public class PopupMenu : MonoBehaviour
{
    public GameObject popupMenuUI;
    public static bool isPopupOpen;
    private GameObject _inventoryCanvas;

    public virtual void Awake()
    {
        popupMenuUI.SetActive(false);
        _inventoryCanvas = GameObject.Find("Inventorycanvas");
    }

    public virtual void OpenPopupMenu()
    {
        FindObjectOfType<AudioManager>().PlaySound("Open popup");
        popupMenuUI.SetActive(true);
        isPopupOpen = true;
        _inventoryCanvas.SetActive(false);
    }

    public virtual void ClosePopupMenu()
    {
        FindObjectOfType<AudioManager>().PlaySound("Close popup");
        popupMenuUI.SetActive(false);
        isPopupOpen = false;
        _inventoryCanvas.SetActive(true);
    }
}
