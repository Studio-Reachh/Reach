using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltCylinder : MonoBehaviour
{
    public float MaxSpeed;
    private float _currentSpeed;

    [SerializeField]
    private Transform _leftPos, _rightPos;

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

        RaycastHit2D rayHitInfo = Physics2D.Raycast(transform.position, rayDirection, 1.1f, LayerMask.GetMask("Player"));
        if (rayHitInfo)
        {
            return;
        }

        transform.position = new Vector3(moveTowardsPos_X, transform.position.y, transform.position.z);
    }
}