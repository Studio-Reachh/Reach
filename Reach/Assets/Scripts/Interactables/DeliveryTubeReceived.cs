using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliveryTubeReceived : MonoBehaviour
{
    private bool _hasCollided;
    private bool _hasBeenDelivered;

    public GameObject GameobjectToReceive;
    public float DistanceForCollision;

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

        if (!_hasCollided && Vector2.Distance(transform.position, GameobjectToReceive.transform.position) < DistanceForCollision)
        {
            _hasCollided = true;
        }

        if (_hasCollided)
        {
            FindObjectOfType<AudioManager>().PlaySound("Item placed right");
            SaveHandler.SaveLevel("Pipe[image]", "HasBeenDelivered", true);
            Destroy(GameobjectToReceive);
        }
    }
}
