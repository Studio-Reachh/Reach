using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lights : MonoBehaviour
{
    private Image _imageRenderer;
    private SpriteRenderer _spriteRenderer;

    [Header("sprites")]
    public Sprite greenLight;

    [Header("Lights Turn Green When")]
    public string LevelWherePuzzleHasBeenSolved;
    public string NameOfPuzzleThatHasBeenSolved;
    public string NameOfItemPropertyThatHasBeenSolved;

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _imageRenderer = GetComponent<Image>();
    }

    void Update()
    {
        if (SaveHandler.GetValueByProperty(LevelWherePuzzleHasBeenSolved, NameOfPuzzleThatHasBeenSolved, NameOfItemPropertyThatHasBeenSolved, out bool hasMorsecodeMessage))
        {
            if (greenLight)
            {
                FindObjectOfType<AudioManager>().PlaySound("Light change");

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
