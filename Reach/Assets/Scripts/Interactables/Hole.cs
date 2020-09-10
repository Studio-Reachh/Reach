using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : Interactable
{
    [SerializeField]
    private SpriteRenderer _plankSpriteRenderer;

    [SerializeField]
    private HoleType _holeType;

    private bool _hasPlank = false;

    public override bool Interact(Item item)
    {
        bool successfulInteraction = false;

        if (item && !_hasPlank && item.name.ToLower() == "woodenplank")
        {
            successfulInteraction = true;
            _hasPlank = true;

            _plankSpriteRenderer.enabled = true;

            if (_holeType == HoleType.Ceilling)
            {
                ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
                if (particleSystem)
                {
                    particleSystem.Stop();
                }
            }
        }

        return successfulInteraction;
    }

    private enum HoleType
    {
        Ceilling,
        Floor
    }
}
