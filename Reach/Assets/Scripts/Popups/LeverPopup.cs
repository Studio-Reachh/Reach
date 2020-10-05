using UnityEngine;

public class LeverPopup : PopupMenu
{
    public static bool IsLeverDown = true;

    [Header("Lever Images")]
    public GameObject LeverUpImage;
    public GameObject LeverDownImage;

    [Header("Lever Sprites")]
    public SpriteRenderer LeverInteractable;
    public Sprite LeverUp;
    public Sprite LeverDown;
    public void OnLeverDown()
    {
        if (IsLeverDown)
        {
            if (LeverDownImage && LeverUpImage)
            {
                LeverUpImage.SetActive(false);
                LeverDownImage.SetActive(true);
            }

            if (LeverInteractable && LeverUp && LeverDown)
            {
                LeverInteractable.sprite = LeverDown;
            }
            IsLeverDown = false;
        }
        else
        {
            if (LeverDownImage && LeverUpImage)
            {
                LeverUpImage.SetActive(true);
                LeverDownImage.SetActive(false);
            }

            if (LeverInteractable && LeverUp && LeverDown)
            {
                LeverInteractable.sprite = LeverUp;
            }
            IsLeverDown = true;
        }

       
    }
}
