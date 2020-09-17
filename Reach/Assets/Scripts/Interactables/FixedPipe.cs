using UnityEngine;
using UnityEngine.SceneManagement;

public class FixedPipe : Interactable
{
    [SerializeField]
    private Renderer _pipeSpriteRenderer;

    private bool _hasPipe = false;
    void Awake()
    {
        _pipeSpriteRenderer = GetComponent<Renderer>();
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "HasPipe", out bool hasPipe))
        {
            _hasPipe = hasPipe;
            AddPipe();
        }
    }

    private void AddPipe()
    {
        _pipeSpriteRenderer.enabled = true;
    }

    public override bool Interact(Item item)
    {
        bool succesfulInteraction = false;

        if (!_hasPipe && item && item.name.ToLower() == "loosepipe")
        {
            succesfulInteraction = true;
            _hasPipe = true;

            AddPipe();
            SaveHandler.SaveLevel(this.name, "HasPipe", true);
        }

        return succesfulInteraction;
    }
}
