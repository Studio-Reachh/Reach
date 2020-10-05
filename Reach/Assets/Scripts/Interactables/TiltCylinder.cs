﻿using UnityEngine;

public class TiltCylinder : MonoBehaviour
{
    public Axis Axis;
    public float MaxSpeed;
    private float _currentSpeed;

    [SerializeField]
    private Transform _leftPos, _rightPos;

    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        Vector3 _tiltVector = Input.acceleration;
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

            if (rayHit.transform.parent.gameObject == this.gameObject)//Check if de root.gameobject werkt
            {
                continue;
            }

            if (rayHit.transform != null && (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Ladder") || (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle") && rayHit.transform.tag == "Barrier") || rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Player")))
            {
                print(rayHit.transform.name);
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
                    print(rayHit.transform.name);
                    return;
                }
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
