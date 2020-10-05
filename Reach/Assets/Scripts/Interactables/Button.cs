using UnityEngine;
public class Button : MonoBehaviour
{
    private SpriteRenderer _spriterenderer;

    [HideInInspector]
    public bool IsButtonDown;

    [Header("Button Images")]
    public Sprite ButtonPressed;
    public Sprite ButtonUnpressed;

    private void Awake()
    {
        _spriterenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        RaycastHit2D[] rayHitInfo = Physics2D.RaycastAll(transform.position, Vector2.up, 2f);

        IsButtonDown = false;

        for (int i = 0; i < rayHitInfo.Length; i++)
        {
            RaycastHit2D rayHit = rayHitInfo[i];

            if (rayHit.collider != null)
            {
                _spriterenderer.sprite = ButtonPressed;
                IsButtonDown = true;
            }
        }

        if (!IsButtonDown)
        {
            _spriterenderer.sprite = ButtonUnpressed;
        }
    }
}