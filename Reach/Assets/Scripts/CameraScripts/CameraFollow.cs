using UnityEngine;

public enum LockedCameraAxis
{
    None,
    X,
    Y
}

public class CameraFollow : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    public Transform target;
    public Vector2 offset;

    public LockedCameraAxis LockedCameraAxis;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        transform.position = new Vector3(_playerMovement.transform.position.x, _playerMovement.transform.position.y, transform.position.z) + new Vector3(offset.x, offset.y, 0);
    }

    private void Update()
    {
        Vector3 posToUse = Vector3.MoveTowards(transform.position, target.position + new Vector3(offset.x, offset.y, 0), (_playerMovement.BaseSpeed * 5) * Time.deltaTime);
        if (LockedCameraAxis == LockedCameraAxis.X)
        {
            transform.position = new Vector3(transform.position.x, posToUse.y, transform.position.z);
        }
        else if (LockedCameraAxis == LockedCameraAxis.Y)
        {
            transform.position = new Vector3(posToUse.x, transform.position.y, transform.position.z);
        }
        else if (LockedCameraAxis == LockedCameraAxis.None)
        {
            transform.position = new Vector3(posToUse.x, posToUse.y, transform.position.z);
        }

        //transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
    }
}
