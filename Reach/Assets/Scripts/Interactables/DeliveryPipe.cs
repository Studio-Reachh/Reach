using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeliveryPipe : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    private Image _image;

    [Header("MoveSpeed")]
    public float MoveSpeed;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _image = gameObject.GetComponent<Image>();

        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "Pipe[image]", "HasBeenDelivered", out bool hasBeenDelivered))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _movement = new Vector2(Input.acceleration.x, Input.acceleration.y);
    }

    private void FixedUpdate()
    {
        if (PopupMenu.isPopupOpen && SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "DeliveryTube", "HasTube", out bool hasTube))
        {
            _image.enabled = true;
            MovePipe(_movement);
        }
    }

    private void MovePipe(Vector2 direction)
    {
        _rigidbody.velocity = direction * MoveSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "PipeDelivered")
        {
            FindObjectOfType<AudioManager>().PlaySound("Item placed right");
            SaveHandler.SaveLevel(this.name, "HasBeenDelivered", true);
            Destroy(gameObject);
        }
    }
}
