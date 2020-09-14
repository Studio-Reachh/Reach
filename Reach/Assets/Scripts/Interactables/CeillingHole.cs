using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CeillingHole : Interactable
{
    [SerializeField]
    private SpriteRenderer _plankSpriteRenderer;

    private bool _hasPlank = false;

    void Awake()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "HasPlank", out bool hasPlank))
        {
            _hasPlank = hasPlank;
            AddPlank();
        }
    }

    private void AddPlank()
    {
        _plankSpriteRenderer.enabled = true;
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        if (particleSystem)
        {
            particleSystem.Stop();
        }
    }

    public override bool Interact(Item item)
    {
        bool successfulInteraction = false;

        if (!_hasPlank && item && item.name.ToLower() == "woodenplank")
        {
            successfulInteraction = true;
            _hasPlank = true;

            AddPlank();
            SaveHandler.SaveLevel(this.name, "HasPlank", true);
        }

        return successfulInteraction;
    }
}
