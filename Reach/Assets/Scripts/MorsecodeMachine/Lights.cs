using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lights : MonoBehaviour
{
    [Header("sprites")]
    public Sprite greenLight;
    private Image _imageRenderer;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _imageRenderer = GetComponent<Image>();
    }

    void Update()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, "MorsecodeMachine[canvas]", "MorsecodeSend", out bool hasMorsecodeMessage))
        {
            if (greenLight)
            {
                if (_spriteRenderer)
                {
                    _spriteRenderer.sprite = greenLight;
                }
                else
                {
                    _imageRenderer.sprite = greenLight;
                }
            }
        }
    }
}
