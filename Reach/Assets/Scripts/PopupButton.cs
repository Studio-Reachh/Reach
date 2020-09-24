public class PopupButton : Interactable
{
    public PopupMenu PopupMenu;

    public override bool Interact(Item item)
    {
        bool successfulInteraction = true;

        PopupMenu.OpenPopupMenu();

        return successfulInteraction;
    }
}
