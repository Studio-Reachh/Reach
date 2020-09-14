using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image _imageElementForItem;

    private Item _item;
    public Item Item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;

            if (!_imageElementForItem)
            {
                _imageElementForItem = GetComponentInChildren<Image>();
            }

            if (_item)
            {
                _imageElementForItem.sprite = Item.Image;
            }
            else
            {
                _imageElementForItem.sprite = null;
            }
        }
    }

    public static bool IsDragging;
    private static GameObject DraggingUIItemGO;
    private static Image DragGOImage;

    private void Awake()
    {
        _imageElementForItem = gameObject.transform.Find("ItemSlot (Image)").GetComponent<Image>();

        if (!DraggingUIItemGO)
        {
            DraggingUIItemGO = GameObject.Find("DraggingUIItem (Image)");
            DraggingUIItemGO.SetActive(false);

            DragGOImage = DraggingUIItemGO.GetComponent<Image>();
            DragGOImage.raycastTarget = false;
        }
    }

    public void AddItem(Item item)
    {
        Item = item;
        SaveHandler.SaveInventory();
    }

    public void RemoveItem()
    {
        Item = null;
        SaveHandler.SaveInventory();
    }

    private void DragItem()
    {
        if (!Item)
        {
            return;
        }

        IsDragging = true;

        DraggingUIItemGO.SetActive(true);
        DragGOImage.sprite = Item.Image;

        _imageElementForItem.enabled = false;
    }

    private void StopDragItem()
    {
        IsDragging = false;

        DraggingUIItemGO.SetActive(false);
        DragGOImage.sprite = null;

        _imageElementForItem.enabled = true;
    }

    void Update()
    {
        if (IsDragging)
        {
            DraggingUIItemGO.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DragItem();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool itemsSuccessfulyUsedForInteraction = false;

        Interactable interactable = Interactable.GetInteractableAtMousePosition();
        if (interactable)
        {
            ItemSlot itemSlot = eventData.pointerPress.GetComponentInChildren<ItemSlot>();//pointerPress vs pointerDrag?
            if (itemSlot && itemSlot.Item)
            {
                itemsSuccessfulyUsedForInteraction = interactable.Interact(itemSlot.Item);
            }
        }

        if (itemsSuccessfulyUsedForInteraction && Item.DestroyOnSuccessfulUse)
        {
            RemoveItem();
        }

        StopDragItem();
    }
}
