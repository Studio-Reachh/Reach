using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTube : Interactable
{
    public override bool Interact(Item item)
    {
        bool succesfulInteraction = false;

        if(!item)
        {
            //open popup
        } else
        {
            //has interactable
            //if
            succesfulInteraction = true;
        }

        return succesfulInteraction;
    }
}
