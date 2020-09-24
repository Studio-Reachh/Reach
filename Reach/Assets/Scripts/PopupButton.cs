public class PopupButton : Interactable
{
    public PopupMenu PopupMenu;

    public override bool Interact(Item item)
    {
        if (item)
        {
            return false;
        }

        PopupMenu.OpenPopupMenu();
        return true;
    }
}