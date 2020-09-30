using UnityEngine;

public class Button : MonoBehaviour
{
    private SpriteRenderer _spriterenderer;
    private bool _barrierLifted;
    private float _originalXPos;
    private float _originalYPos;

    private Vector2 _target;

    [Header("Sprites")]
    public Sprite ButtonPressed;
    public Sprite ButtonUnpressed;

    [Header("Hoist Barrier")]
    public GameObject Barrier;
    public float Speed;

    public BarrierAxis BarrierAxis;
    public float xPos;
    public float yPos;


    void Awake()
    {
        _spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        _originalXPos = Barrier.transform.position.x;
        _originalYPos = Barrier.transform.position.y;
    }

    private void Update()
    {
        RaycastHit2D[] rayHitInfo = Physics2D.RaycastAll(transform.position, Vector3.up, 1.1f);

        _barrierLifted = false;

        for (int i = 0; i < rayHitInfo.Length; i++)
        {
            RaycastHit2D rayHit = rayHitInfo[i];

            if (rayHit)
            {
                LiftBarrier();
                _barrierLifted = true;
            }
        }

        if (!_barrierLifted)
        {
            LowerBarrier();
        }
    }

    private void LiftBarrier()
    {
        if (ButtonPressed)
        {
            _spriterenderer.sprite = ButtonPressed;
        }

        if (BarrierAxis == BarrierAxis.Horizontal)
        {
            _target = new Vector2(xPos, _originalYPos);
        } 
        else if (BarrierAxis == BarrierAxis.Vertical)
        {
            _target = new Vector2(_originalXPos, yPos);
        }

        Barrier.transform.position = Vector2.MoveTowards(Barrier.transform.position, _target, Speed * Time.deltaTime);
    }

    private void LowerBarrier()
    {
        if (ButtonUnpressed)
        {
            _spriterenderer.sprite = ButtonUnpressed;
        }
        
        _target = new Vector2(_originalXPos, _originalYPos);

        Barrier.transform.position = Vector2.MoveTowards(Barrier.transform.position, _target, Speed * Time.deltaTime);
    }
}
public enum BarrierAxis
{
    Horizontal,
    Vertical
}