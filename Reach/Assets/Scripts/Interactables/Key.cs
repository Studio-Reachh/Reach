using UnityEngine;

public class Key : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movementOnAxis;
    private bool _hasKeyFallen = false;
    private float _slideBackKey;

    public float SlideBackPosX = 2;

    public float Speed;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_hasKeyFallen)
        {
            _movementOnAxis = new Vector2(Input.acceleration.x, 0);
        } 
        else
        {
            _movementOnAxis = new Vector2(Input.acceleration.x, Input.acceleration.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "floor")
        {
            _hasKeyFallen = true;
        }

        if(collision.gameObject.tag == "Barrier")
        {
            var topOfBarrier = collision.contacts[0].normal;

            if (topOfBarrier.y < 0)
            {
                _slideBackKey = transform.position.x - SlideBackPosX;
                transform.position = new Vector2(_slideBackKey, transform.position.y);
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movementOnAxis * Speed * Time.fixedDeltaTime;
    }
}
