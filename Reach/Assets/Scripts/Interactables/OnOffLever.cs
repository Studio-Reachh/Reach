using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnOffLever : Interactable
{
    public Interactable InteractableToInteractWith;
    public Item LeverItem;
    public bool HasLever;
    public SpriteRenderer LeverSpriteRenderer;

    public bool IsOn;

    private void Awake()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "IsOn", out bool isOn))
        {
            IsOn = isOn;
        }

        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "HasLever", out bool hasLever))
        {
            HasLever = hasLever;
        }

        if (HasLever)
        {
            AddLever();
        }

        SetLeverState();
    }

    public override bool Interact(Item item)
    {
        bool successfulInteraction = false;
        if (!HasLever)
        {
            //This box does not have a lever, add lever and set it to the current lever state
            if (item && item == LeverItem)
            {
                successfulInteraction = true;

                SaveHandler.SaveLevel(name, "HasLever", true);
                AddLever();
                SetLeverState();
            }
        }
        else
        {
            if (item)
            {
                return false;
            }

            //No item was provided and there is a lever present
            IsOn = !IsOn;
            InteractableToInteractWith.Interact(LeverItem);

            SetLeverState();
        }

        return successfulInteraction;
    }

    public void SetLeverState()
    {
        if (HasLever)
        {
            SaveHandler.SaveLevel(name, "IsOn", IsOn);
            if (!IsOn)
            {
                LeverSpriteRenderer.transform.position = new Vector3(-2.217f, 3.167f, -2);
            }
            else
            {
                LeverSpriteRenderer.transform.position = new Vector3(-2.217f, 2.647f, -2);
            }
        }
    }

    public void AddLever()
    {
        HasLever = true;
        LeverSpriteRenderer.enabled = true;
    }
}
