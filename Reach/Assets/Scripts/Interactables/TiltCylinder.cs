﻿using UnityEngine;

public class TiltCylinder : MonoBehaviour
{
    public Axis Axis;
    public float MaxSpeed;
    private float _currentSpeed;
    private bool _isMoving = false;
    private bool _isSoundPlaying = false;

    public Animator CylinderAnimator;

    [SerializeField]
    private Transform _leftPos, _rightPos;

    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (PopupMenu.isPopupOpen)
        {
            if (CylinderAnimator)
            {
                CylinderAnimator.speed = 0;
            }

            return;
        }

        Vector3 _tiltVector = Input.acceleration;

        if (CylinderAnimator)
        {
            CylinderAnimator.speed = Mathf.Abs(_tiltVector.x);
            if (CylinderAnimator.speed > 1)
            {
                CylinderAnimator.speed = 1;
            }
        }

        if (_tiltVector.x == 0)
        {
            return;
        }

        _currentSpeed = ((MaxSpeed / 100f * (float)_tiltVector.x) * 100f) * Time.deltaTime;

        float moveTowardsPos_X = transform.position.x;
        Vector2 rayDirection = Vector2.zero;

        if (_tiltVector.x < 0)
        {
            moveTowardsPos_X = Vector2.MoveTowards(transform.position, _leftPos.position, Mathf.Abs(_currentSpeed)).x;
            rayDirection = Vector2.left;
        }
        else if (_tiltVector.x > 0)
        {
            moveTowardsPos_X = Vector2.MoveTowards(transform.position, _rightPos.position, Mathf.Abs(_currentSpeed)).x;
            rayDirection = Vector2.right;
        }

        if (_tiltVector.x > 0.2 || _tiltVector.x < -0.2)
        {
            _isMoving = true;
        } else
        {
            _isMoving = false;
        }

        if (_isMoving)
        {
            if (!_isSoundPlaying)
            {
                FindObjectOfType<AudioManager>().PlaySound("Rolling Cylinder");
                _isSoundPlaying = true;
            }
        } else
        {
            FindObjectOfType<AudioManager>().StopSound("Rolling Cylinder");
            _isSoundPlaying = false;
        }

        int layerMask = -1;

        if (Axis == Axis.Horizontal)
        {
            layerMask = LayerMask.GetMask("Player");
        }
        else
        {
            layerMask = (1 << LayerMask.NameToLayer("Ladder") | (1 << LayerMask.NameToLayer("Obstacle")));
        }

        RaycastHit2D[] rayHitInfo = Physics2D.RaycastAll(transform.position, rayDirection, 1.1f, layerMask);
        for (int i = 0; i < rayHitInfo.Length; i++)
        {
            RaycastHit2D rayHit = rayHitInfo[i];

            if (rayHit.transform.parent && rayHit.transform.parent.gameObject == this.gameObject)//Check if de root.gameobject werkt
            {
                continue;
            }

            if (rayHit.transform != null && (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Ladder") || (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle") && rayHit.transform.tag == "Barrier") || rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Player")))
            {
                if (CylinderAnimator)
                {
                    CylinderAnimator.speed = 0;
                }
                
                return;
            }
        }

        if (spriteRenderer)
        {
            RaycastHit2D[] rayHitInfo2 = Physics2D.RaycastAll(transform.position + new Vector3(0, spriteRenderer.sprite.bounds.size.y / 2f, 0), rayDirection, 1.1f, layerMask);
            for (int i = 0; i < rayHitInfo2.Length; i++)
            {
                RaycastHit2D rayHit = rayHitInfo2[i];

                if (rayHit.transform.parent.gameObject == this.gameObject)//Check if de root.gameobject werkt
                {
                    continue;
                }

                if (rayHit.transform != null && (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Ladder") || (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle") && rayHit.transform.tag == "Barrier") || rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Player")))
                {
                    if (CylinderAnimator)
                    {
                        CylinderAnimator.speed = 0;
                    }

                    return;
                }
            }
        }

        if (CylinderAnimator)
        {
            if (_tiltVector.x < 0)
            {
                CylinderAnimator.SetBool("IsMovingRight", false);
            }
            else if (_tiltVector.x > 0)
            {
                CylinderAnimator.SetBool("IsMovingRight", true);
            }
        }

        transform.position = new Vector3(moveTowardsPos_X, transform.position.y, transform.position.z);
    }
}

public enum Axis
{
    Horizontal,
    Vertical
}
