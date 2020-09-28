using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacingDirection
{
    Left,
    Right
}

public class PlayerMovement : MonoBehaviour
{
    public float BaseSpeed;
    private float _currentSpeed;

    public bool ClickedOnLadder = false;
    public bool _isMovingOnLadder = false;

    public bool DestinationReached = false;

    private Vector3 _targetPosition;
    public Vector3 TargetPosition
    {
        get
        {
            return _targetPosition;
        }
        set
        {
            _targetPosition = value;
            DestinationReached = false;
        }
    }

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;

    public ParticleSystem leftFootParticleSystem, rightFootParticleSystem;
    public FacingDirection FacingDirection;

    private void OnApplicationQuit()
    {
        PlayerData playerData = new PlayerData()
        {
            FacingDirection = this.FacingDirection,
            X_Pos = transform.position.x,
            Y_Pos = transform.position.y,
            Z_Pos = transform.position.z
        };

        SaveHandler.SavePlayerData(playerData);
    }

    private void Awake()
    {
        //PlayerData playerData = SaveHandler.GetPlayerData();
        //if (playerData != null)
        //{
        //    transform.position = new Vector3(playerData.X_Pos, playerData.Y_Pos, playerData.Z_Pos);
        //    this.FacingDirection = playerData.FacingDirection;
        //}

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        TargetPosition = transform.position;
    }

    private Interactable _interactableToMoveTowards;


    public bool IsGameobjectAtPosition(Vector2 position, string layerName, out RaycastHit2D raycastHit)
    {
        //Vector2 mousePosAsworldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        raycastHit = Physics2D.Raycast(position, Vector2.zero, Mathf.Infinity, LayerMask.GetMask(layerName));
        return raycastHit;
    }

    private void Update()
    {
        if (PopupMenu.isPopupOpen)
        {
            _currentSpeed = 0;
            return;
        }
        else
        {
            _currentSpeed = BaseSpeed;
        }
        _spriteRenderer.flipX = FacingDirection == FacingDirection.Left ? true : false;
        if (!_isMovingOnLadder && !ItemSlot.IsDragging && Input.GetKeyDown(KeyCode.Mouse0) && !_animator.GetBool("IsTurning") && !_animator.GetBool("IsGrabbingUp"))
        {
            //Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //if (_boxCollider2D.bounds.Contains(new Vector3(clickPos.x, clickPos.y, transform.position.z)))
            //{
            //    print("clicked on player");
            //    return;
            //}
            
            Interactable interactableToMoveTowards = Interactable.GetInteractableAtMousePosition();
            if (interactableToMoveTowards)
            {
                Collider2D collider = interactableToMoveTowards.GetComponent<Collider2D>();
                if (collider)
                {
                    if (transform.position.x <= collider.bounds.min.x)
                    {
                        TargetPosition = collider.bounds.min;
                    }
                    else if (transform.position.x >= collider.bounds.max.x)
                    {
                        TargetPosition = collider.bounds.max;
                    }
                    else
                    {
                        TargetPosition = transform.position;
                    }
                }
                else
                {
                    TargetPosition = interactableToMoveTowards.transform.position;
                }

                _interactableToMoveTowards = interactableToMoveTowards;
            }
            else
            {
                TargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _interactableToMoveTowards = null;
            }

            Vector2 rayDirection = Vector2.zero;
            if (TargetPosition.x < transform.position.x)
            {
                if (FacingDirection == FacingDirection.Right)
                {
                    _animator.SetBool("IsTurning", true);
                    _animator.SetBool("IsWalking", false);
                }

                rayDirection = Vector2.left;
            }
            else if (TargetPosition.x > transform.position.x)
            {
                if (FacingDirection == FacingDirection.Left)
                {
                    _animator.SetBool("IsTurning", true);
                    _animator.SetBool("IsWalking", false);
                }

                rayDirection = Vector2.right;
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, Mathf.Abs(TargetPosition.x - transform.position.x), LayerMask.GetMask("Obstacle"));
            if (hit)
            {
                if (hit.distance < 1)
                {
                    print(hit.distance);
                }

                if (rayDirection == Vector2.left)
                {
                    TargetPosition = new Vector2(hit.point.x + _boxCollider2D.bounds.size.x / 1.88f, TargetPosition.y);
                }
                else if (rayDirection == Vector2.right)
                {
                    TargetPosition = new Vector2(hit.point.x - _boxCollider2D.bounds.size.x / 1.88f, TargetPosition.y);
                }

                _interactableToMoveTowards = null;
            }

            if (IsGameobjectAtPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition), "Ladder", out RaycastHit2D rayHit))
            {
                ClickedOnLadder = true;
            }
            else
            {
                ClickedOnLadder = false;
            }
        }

        if (!DestinationReached)
        {
            if (transform.position.x != TargetPosition.x && !_animator.GetBool("IsTurning") && !_animator.GetBool("IsGrabbingUp"))
            {
                _animator.SetBool("IsWalking", true);

                if (_isMovingOnLadder)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(TargetPosition.x, TargetPosition.y, transform.position.z), Time.deltaTime * _currentSpeed);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(TargetPosition.x, transform.position.y, transform.position.z), Time.deltaTime * _currentSpeed);
                }

                Vector2 rayDirection = FacingDirection == FacingDirection.Left ? Vector2.left : Vector2.right;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, Mathf.Abs(TargetPosition.x - transform.position.x), LayerMask.GetMask("Obstacle"));
                if (hit)
                {
                    if (rayDirection == Vector2.left)
                    {
                        TargetPosition = new Vector2(hit.point.x + _boxCollider2D.bounds.size.x / 1.88f, TargetPosition.y);
                    }
                    else if (rayDirection == Vector2.right)
                    {
                        TargetPosition = new Vector2(hit.point.x - _boxCollider2D.bounds.size.x / 1.88f, TargetPosition.y);
                    }

                    _interactableToMoveTowards = null;
                }
            }
            else
            {
                if (_isMovingOnLadder)
                {
                    if (transform.position.x == TargetPosition.x && transform.position.y == TargetPosition.y)
                    {
                        _animator.SetBool("IsWalking", false);
                        _isMovingOnLadder = false;
                        DestinationReached = true;
                    }
                }
                else
                {
                    if (transform.position.x == TargetPosition.x)
                    {
                        if (ClickedOnLadder && IsGameobjectAtPosition(transform.position, "Ladder", out RaycastHit2D raycastHit))
                        {
                            _isMovingOnLadder = true;
                            if (raycastHit.transform.name.ToLower() == "under")
                            {
                                //Search for above
                                TargetPosition = raycastHit.transform.parent.Find("Above").position;
                            }
                            else if (raycastHit.transform.name.ToLower() == "above")
                            {
                                //Search for under
                                TargetPosition = raycastHit.transform.parent.Find("Under").position;
                            }
                        }
                        else
                        {
                            if (_interactableToMoveTowards)
                            {
                                if (_interactableToMoveTowards.UseGrabAnimOnInteract)
                                {
                                    _animator.SetBool("IsGrabbingUp", true);
                                }
                                else
                                {
                                    InteractWithInteractable();
                                }
                            }

                            DestinationReached = true;
                        }

                        ClickedOnLadder = false;
                        _animator.SetBool("IsWalking", false);
                    }
                }
            }
        }
    }

    public void EndOfTurningAnim()
    {
        if (FacingDirection == FacingDirection.Right)
        {
            FacingDirection = FacingDirection.Left;
        }
        else
        {
            FacingDirection = FacingDirection.Right;
        }

        _animator.SetBool("IsTurning", false);
        _animator.SetBool("IsWalking", false);
    }

    public void EndGrabAnim()
    {
        _animator.SetBool("IsGrabbingUp", false);

        if (_interactableToMoveTowards)
        {
            InteractWithInteractable();
        }
    }

    private void InteractWithInteractable()
    {
        _interactableToMoveTowards.Interact(null);
        _interactableToMoveTowards = null;
    }

    public void WalkStepParticles(string foot)
    {
        //if (_spriteRenderer.flipX)
        //{
        //    if (foot.ToLower() == "left")
        //    {
        //        rightFootParticleSystem.Play();
        //    }
        //    else if (foot.ToLower() == "right")
        //    {
        //        leftFootParticleSystem.Play();
        //    }
        //}
        //else
        //{
        //    if (foot.ToLower() == "left")
        //    {
        //        leftFootParticleSystem.Play();
        //    }
        //    else if (foot.ToLower() == "right")
        //    {
        //        rightFootParticleSystem.Play();
        //    }
        //}
    }
}
