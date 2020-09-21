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
    public float speed;

    public Vector3 _targetPosition;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;

    public ParticleSystem leftFootParticleSystem, rightFootParticleSystem;
    public FacingDirection FacingDirection;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        _targetPosition = transform.position;
    }

    private Interactable _interactableToMoveTowards;

    private void Update()
    {
        _spriteRenderer.flipX = FacingDirection == FacingDirection.Left ? true : false;

        if (Input.GetKeyDown(KeyCode.Mouse0) && !_animator.GetBool("IsTurning") && !_animator.GetBool("IsGrabbingUp"))
        {
            Interactable interactableToMoveTowards = Interactable.GetInteractableAtMousePosition();
            if (interactableToMoveTowards)
            {
                Collider2D collider = interactableToMoveTowards.GetComponent<Collider2D>();
                if (collider)
                {
                    if (transform.position.x <= collider.bounds.min.x)
                    {
                        _targetPosition = collider.bounds.min;
                    }
                    else if (transform.position.x >= collider.bounds.max.x)
                    {
                        _targetPosition = collider.bounds.max;
                    }
                    else
                    {
                        _targetPosition = transform.position;
                    }
                }
                else
                {
                    _targetPosition = interactableToMoveTowards.transform.position;
                }

                _interactableToMoveTowards = interactableToMoveTowards;
            }
            else
            {
                _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _interactableToMoveTowards = null;
            }

            Vector2 rayDirection = Vector2.zero;
            if (_targetPosition.x < transform.position.x)
            {
                if (FacingDirection == FacingDirection.Right)
                {
                    _animator.SetBool("IsTurning", true);
                    _animator.SetBool("IsWalking", false);
                }

                rayDirection = Vector2.left;
            }
            else if (_targetPosition.x > transform.position.x)
            {
                if (FacingDirection == FacingDirection.Left)
                {
                    _animator.SetBool("IsTurning", true);
                    _animator.SetBool("IsWalking", false);
                }

                rayDirection = Vector2.right;
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, Mathf.Abs(_targetPosition.x - transform.position.x), LayerMask.GetMask("Obstacle"));
            if (hit)
            {
                if (rayDirection == Vector2.left)
                {
                    _targetPosition = new Vector2(hit.point.x + _boxCollider2D.bounds.size.x / 2, _targetPosition.y);
                }
                else if (rayDirection == Vector2.right)
                {
                    _targetPosition = new Vector2(hit.point.x - _boxCollider2D.bounds.size.x / 2, _targetPosition.y);
                }

                _interactableToMoveTowards = null;
            }
        }

        if (transform.position.x != _targetPosition.x && !_animator.GetBool("IsTurning") && !_animator.GetBool("IsGrabbingUp"))
        {
            _animator.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetPosition.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
        }
        else if (transform.position.x == _targetPosition.x)
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

            _animator.SetBool("IsWalking", false);
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
