using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Vector3 _targetPosition;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;

    public ParticleSystem leftFoot, rightFoot;

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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Interactable interactableToMoveTowards = Interactable.GetInteractableAtMousePosition();
            if (interactableToMoveTowards)
            {
                Collider2D collider = interactableToMoveTowards.GetComponent<Collider2D>();
                if (collider)
                {
                    //TODO: Weet nog niet zeker of dit is wat ik wil, kijk of er een andere manier is
                    _targetPosition = new Vector2(collider.ClosestPoint(transform.position).x, _targetPosition.y);
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

            if (_targetPosition.x < transform.position.x)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }

            Vector2 rayDirection = _spriteRenderer.flipX == true ? Vector2.left : Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, Mathf.Abs(_targetPosition.x - transform.position.x), LayerMask.GetMask("Obstacle"));
            if (hit)
            {
                if (_spriteRenderer.flipX)
                {
                    _targetPosition = new Vector2(hit.point.x + _boxCollider2D.bounds.size.x / 2, _targetPosition.y);
                }
                else
                {
                    _targetPosition = new Vector2(hit.point.x - _boxCollider2D.bounds.size.x / 2, _targetPosition.y);
                }

                _interactableToMoveTowards = null;
                return;
            }
        }

        if (transform.position.x != _targetPosition.x)
        {
            _animator.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetPosition.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
        }
        else if (transform.position.x == _targetPosition.x)
        {
            if (_interactableToMoveTowards)
            {
                _interactableToMoveTowards.Interact(null);
                _interactableToMoveTowards = null;
            }

            _animator.SetBool("IsWalking", false);
        }
    }

    public void WalkStepParticles(string foot)
    {
        if (_spriteRenderer.flipX)
        {
            if (foot.ToLower() == "left")
            {
                rightFoot.Play();
            }
            else if (foot.ToLower() == "right")
            {
                leftFoot.Play();
            }
        }
        else
        {
            if (foot.ToLower() == "left")
            {
                leftFoot.Play();
            }
            else if (foot.ToLower() == "right")
            {
                rightFoot.Play();
            }
        }
    }
}
