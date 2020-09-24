using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool UseGrabAnimOnInteract;
    /// <summary>
    /// Interact with the Interactable
    /// </summary>
    /// <param name="item">Item to give to the interactable</param>
    /// <returns>True if the interaction was successful</returns>
    public abstract bool Interact(Item item);

    public static Interactable GetInteractableAtMousePosition()
    {
        Interactable foundInteractable = null;

        Vector2 mousePosAsworldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHitInfo = Physics2D.Raycast(mousePosAsworldPoint, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Interactable"));

        if (rayHitInfo)
        {
            foundInteractable = rayHitInfo.transform.GetComponent<Interactable>();//TODO: Maybe GetComponentsInChildren, for now look for the interactable in the root of the GameObject
        }

        return foundInteractable;
    }
}
