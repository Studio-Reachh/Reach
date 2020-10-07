using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    void Update()
    {
        if (!SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "IsDoorOpen", out bool IsDoorOpen))
        {
            if (!PopupMenu.isPopupOpen && SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "Pipe[image]", "HasBeenDelivered", out bool hasBeenDelivered))
            {
                GetComponent<Animator>().Play("DoorAnimation");
                FindObjectOfType<AudioManager>().PlaySound("Electric door");

                SaveHandler.SaveLevel(this.name, "IsDoorOpen", true);
            }
        }
    }
}
