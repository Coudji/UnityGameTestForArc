using FishNet.Object;
using UnityEngine.InputSystem;

public class InputsManager : BaseCharacterManager
{
    private AttackManager _attackManager;
    private InventoryMenu _inventory;

    public override void Start()
    {
        base.Start();
        _attackManager = _character.AttackManager;
        _inventory = _character.InventoryMenu;
    }

    public void OnAttack(InputValue value)
    {
        _attackManager.PerformAttack();
    }

    public void OnOpenInventory(InputValue value) => _inventory.OpenMenu();

    public void OnCloseInventory(InputValue value) => _inventory.CloseMenu();

    public void OnCancel(InputValue value) => _inventory.CloseMenu();
}
