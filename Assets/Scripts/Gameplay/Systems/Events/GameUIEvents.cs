using System;

public static class GameUIEvents
{
    public static event Action<MenuName> OnOpenMenuRequested;
    public static event Action<MenuName> OnCloseMenuRequested;
    public static event Action<InventorySystem> OnInventoryChanged;

    public static void RequestOpen(MenuName menuName) => OnOpenMenuRequested?.Invoke(menuName);

    public static void RequestClose(MenuName menuName) => OnCloseMenuRequested?.Invoke(menuName);

    public static void NotifyInventoryChanged(InventorySystem inventory) =>
        OnInventoryChanged?.Invoke(inventory);
}
