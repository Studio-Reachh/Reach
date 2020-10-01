using UnityEngine;

public class LeverBox : Interactable
{
    private bool _isLeverUp = true;

    [Header("sprites")]
    public Sprite LeverupSprite;
    public Sprite LeverDownSprite;

    [Header("Barrier Y Axis")]
    public float YPositionBarrierUp;
    public float YPositionBarrierDown;
    public float Speed;

    private GameObject[] _barriers;

    private void Awake()
    {
        _barriers = GameObject.FindGameObjectsWithTag("Barrier");
    }
    public override bool Interact(Item item)
    {
        bool succesfullInteraction = false;

        if (_isLeverUp)
        {
            _isLeverUp = false;
        } else
        {
            _isLeverUp = true;
        }

        return succesfullInteraction;
    }

    private void Leverup()
    {
        foreach (var barrier in _barriers)
        {
            if (barrier.name == "barrierUp")
            {
                barrier.transform.position = Vector2.MoveTowards(barrier.transform.position, new Vector2(barrier.transform.position.x, YPositionBarrierUp), Speed * Time.fixedDeltaTime); 
            }
            else
            {
                barrier.transform.position = Vector2.MoveTowards(barrier.transform.position, new Vector2(barrier.transform.position.x, YPositionBarrierDown), Speed * Time.fixedDeltaTime);
            }
        }
    }

    private void LeverDown()
    {
        foreach (var barrier in _barriers)
        {
            if (barrier.name == "barrierUp")
            {
                barrier.transform.position = Vector2.MoveTowards(barrier.transform.position, new Vector2(barrier.transform.position.x, YPositionBarrierDown), Speed * Time.fixedDeltaTime);
            }
            else
            {
                barrier.transform.position = Vector2.MoveTowards(barrier.transform.position, new Vector2(barrier.transform.position.x, YPositionBarrierUp), Speed * Time.fixedDeltaTime);
            }
        }
    }

    private void Update()
    {
        if (_isLeverUp)
        {
            Leverup();
        } else
        {
            LeverDown();
        }
    }
}
