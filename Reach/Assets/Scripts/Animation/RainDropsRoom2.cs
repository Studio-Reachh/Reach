using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDropsRoom2 : MonoBehaviour
{
    public ParticleCollision ParticleCollision;

    private void Awake()
    {
        if (SaveHandler.GetValueByProperty("Room01", "CeillingHole (Empty GO)", "HasPlank", out bool hasPlank))
        {
            if (!hasPlank)
            {
                ParticleCollision.gameObject.SetActive(true);
            }
        }
        else
        {
            ParticleCollision.gameObject.SetActive(true);
        }
    }
}
