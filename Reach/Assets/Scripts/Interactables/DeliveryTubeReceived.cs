using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliveryTubeReceived : MonoBehaviour
{
    public bool HasCollided;
    private bool _hasBeenDelivered;

    public GameObject GameobjectToReceive;
    public float DistanceForCollision;

    [Header("Close Popup")]
    public PopupMenu Popup;

    public void Awake()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "Pipe[image]", "HasBeenDelivered", out bool hasBeenDelivered))
        {
            _hasBeenDelivered = hasBeenDelivered;
            Destroy(GameobjectToReceive);
        }
    }

    public void Update()
    {
        if (_hasBeenDelivered)
        {
            return;
        }

        if (!HasCollided && Vector2.Distance(transform.position, GameobjectToReceive.transform.position) < DistanceForCollision)
        {
            HasCollided = true;
        }

        if (HasCollided)
        {
            FindObjectOfType<AudioManager>().PlaySound("Item placed right");
            SaveHandler.SaveLevel("Pipe[image]", "HasBeenDelivered", true);

            if (Popup)
            {
                Popup.ClosePopupMenu();
            }

            Destroy(GameobjectToReceive);
        }
    }
}
