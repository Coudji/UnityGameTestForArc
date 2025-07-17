using UnityEngine.InputSystem;

namespace Arc.Gameplay.Managers
{
    public class InventoryMenu : BaseMenuManager
    {
        public void OnOpenInventory(InputValue value) => OpenMenu();
    
        public void OnCloseInventory(InputValue value) => CloseMenu();
    
        public void OnCancel(InputValue value) => CloseMenu();
    }
}
