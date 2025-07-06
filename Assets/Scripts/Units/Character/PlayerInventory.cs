using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class PlayerInventory : OwnerBehaviour
{
    [SerializeField]
    private InventoryConfig _config;

    private InventorySystem _inventory;

    public override void OnStartClient()
    {
        base.OnStartClient();
        _inventory = new(_config.MaxSlots);
    }

    [TargetRpc]
    public void TryPickupLoot(NetworkConnection conn, NetworkObject networkObject)
    {
        if (_inventory == null)
        {
            ConfirmLootServerRpc(networkObject, -1);
            return;
        }

        if (networkObject.TryGetComponent<LootableItem>(out var lootable))
        {
            int remainingQuantity = _inventory.AddItem(lootable.GetItem(), lootable.GetQuantity());
            ConfirmLootServerRpc(networkObject, remainingQuantity);
        }
        else
        {
            ConfirmLootServerRpc(networkObject, -1);
        }
    }

    [ServerRpc]
    private void ConfirmLootServerRpc(NetworkObject networkObject, int remainingQuantity)
    {
        if (networkObject.TryGetComponent<LootableItem>(out var lootable))
        {
            if (remainingQuantity == 0)
                lootable.OnLootConfirmed();
            else
                lootable.OnLootCancelled(remainingQuantity);
        }
    }
}
