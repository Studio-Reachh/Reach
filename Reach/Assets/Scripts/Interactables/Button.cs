using UnityEngine;
public class Button : MonoBehaviour
{
    private SpriteRenderer _spriterenderer;

    [HideInInspector]
    public bool IsButtonDown = false;

    [Header("Button Images")]
    public Sprite ButtonPressed;
    public Sprite ButtonUnpressed;

    private void Awake()
    {
        _spriterenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 2f);

        if (hit.collider != null && !IsButtonDown)
        {
            //change sprite
            _spriterenderer.sprite = ButtonPressed;

            //sound effect
            FindObjectOfType<AudioManager>().PlaySound("Button pressed");

            IsButtonDown = true;
            Barrier.HasSoundPlayed = false;
        }

        if (hit.collider == null && IsButtonDown)
        {
            //change sprite
            _spriterenderer.sprite = ButtonUnpressed;

            //sound effect
            FindObjectOfType<AudioManager>().PlaySound("Button released");

            IsButtonDown = false;
            Barrier.HasSoundPlayed = false;
        }
    }
}