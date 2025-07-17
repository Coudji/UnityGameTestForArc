using System;

public static class ItemEvents
{
    public static event Action<Item> OnItemEquipped;

    public static void RaiseItemEquipped(Item equippedItem)
    {
        OnItemEquipped?.Invoke(equippedItem);
    }
}
