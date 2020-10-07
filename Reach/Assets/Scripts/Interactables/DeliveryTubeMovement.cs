using UnityEngine;

public class DeliveryTubeMovement : MonoBehaviour
{
    public AxisTube Axis;
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

        int layerMask = -1;

        if (Axis == AxisTube.Horizontal)
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

            if (rayHit.transform != null && (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Ladder") || (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle") && rayHit.transform.tag == "Barrier") || rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Player")))
            {
                return;
            }
        }

        transform.position = new Vector3(moveTowardsPos_X, transform.position.y, transform.position.z);
    }
}
public enum AxisTube
{
    Horizontal,
    Vertical
}
