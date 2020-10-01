using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    private bool _isDooropen = false;
    // Update is called once per frame
    void Update()
    {
        if (!_isDooropen)
        {
            if (!PopupMenu.isPopupOpen && SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "Pipe[image]", "HasBeenDelivered", out bool hasBeenDelivered))
            {
                GetComponent<Animator>().Play("DoorAnimation");
                _isDooropen = true;
            }
        }
    }
}
