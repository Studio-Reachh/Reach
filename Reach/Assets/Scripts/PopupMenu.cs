using UnityEngine;

public class PopupMenu : MonoBehaviour
{
    public GameObject popupMenuUI;

    public void Awake()
    {
        popupMenuUI.SetActive(false);
    }
    public void OpenPopupMenu()
    {
        popupMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ClosePopupMenu()
    {
        popupMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

}
