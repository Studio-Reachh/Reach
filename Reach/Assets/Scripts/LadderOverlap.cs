using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderOverlap : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public int DefaultOrderLayer, OrderLayerWhilePlayerIsClimbing;

    private void Update()
    {
        if (PlayerMovement.IsMovingOnLadder)
        {
            if (SpriteRenderer)
            {
                SpriteRenderer.sortingOrder = OrderLayerWhilePlayerIsClimbing;
            }
        }
        else
        {
            if (SpriteRenderer)
            {
                SpriteRenderer.sortingOrder = DefaultOrderLayer;
            }
        }
    }
}
