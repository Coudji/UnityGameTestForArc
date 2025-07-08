using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputs : MonoBehaviour
{
    public InventoryMenu Inventory;

    public void OnOpenInventory(InputValue value) => Inventory.OpenMenu();

    public void OnCloseInventory(InputValue value) => Inventory.CloseMenu();

    public void OnCancel(InputValue value) => Inventory.CloseMenu();
}
