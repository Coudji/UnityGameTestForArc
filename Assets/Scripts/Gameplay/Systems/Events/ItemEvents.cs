using System;

namespace Arc.Gameplay.Systems.Events
{
    public static class ItemEvents
    {
        public static event Action<Item> OnItemEquipped;
    
        public static void RaiseItemEquipped(Item equippedItem)
        {
            OnItemEquipped?.Invoke(equippedItem);
        }
    }
}
