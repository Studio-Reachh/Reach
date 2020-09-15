using UnityEngine;
using System.Collections;

public class PopupMenu : MonoBehaviour
{
    public GameObject popupMenuUI;
    protected bool isPopupOpen;
    public void Awake()
    {
        popupMenuUI.SetActive(false);
    }
    public virtual void OpenPopupMenu()
    {
        popupMenuUI.SetActive(true);
      
        isPopupOpen = true;
    }
    public virtual void ClosePopupMenu()
    {
        popupMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPopupOpen = false;
    }
}
