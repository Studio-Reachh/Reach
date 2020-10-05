using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingBarrier : Interactable
{
    public Item LeverItem;
    public Transform AbovePos, UnderPos;
    public bool IsAbove;
    public float Speed;

    private void Awake()
    {
        if (SaveHandler.GetValueByProperty(SceneManager.GetActiveScene().name, name, "IsAbove", out bool isAbove))
        {
            IsAbove = isAbove;
        }

        SetBarrierPos();
    }

    public void SetBarrierPos()
    {
        if (IsAbove)
        {
            transform.position = new Vector3(AbovePos.position.x, AbovePos.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(UnderPos.position.x, UnderPos.position.y, transform.position.z);
        }
    }

    public override bool Interact(Item item)
    {
        //You can only interact when Lever Item is given
        if (item && item == LeverItem)
        {
            IsAbove = !IsAbove;
            SaveHandler.SaveLevel(name, "IsAbove", IsAbove);
        }

        //Always return false
        return false;
    }

    private void Update()
    {
        if (IsAbove && transform.position.y != AbovePos.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, AbovePos.position, Speed * Time.deltaTime);
        }
        else if (!IsAbove && transform.position.y != UnderPos.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, UnderPos.position, Speed * Time.deltaTime);
        }
    }
}
