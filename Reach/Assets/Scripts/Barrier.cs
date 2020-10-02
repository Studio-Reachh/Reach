using UnityEngine;

public class Barrier : MonoBehaviour
{
    private float _slidbackObject;

    [Header("Barrier Positions Y-axis")]
    public float BarrierStartPositionY;
    public float BarrierEndPositionY;
    public float Speed;
    public float SlideObjectBackDistance;

    [Header("Object that you dont want getting stuck")]
    public GameObject TiltObject;
    private void Update()
    {
        RaycastHit2D[] rayHitInfo = Physics2D.RaycastAll(transform.position, Vector2.down, 2f);
        for (int i = 0; i < rayHitInfo.Length; i++)
        {
            RaycastHit2D rayHit = rayHitInfo[i];

            if (TiltObject)
            {
                if (rayHit.transform != null && rayHit.transform.name == TiltObject.name)
                {
                    _slidbackObject = rayHit.transform.position.x - SlideObjectBackDistance;
                    rayHit.transform.position = new Vector2(_slidbackObject, rayHit.transform.position.y);
                }
            }
        }

        if (!PopupMenu.isPopupOpen)
        {
            if (LeverPopup.IsLeverDown)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, BarrierEndPositionY), Speed * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, BarrierStartPositionY), Speed * Time.fixedDeltaTime);
            }
        }
    }
}
