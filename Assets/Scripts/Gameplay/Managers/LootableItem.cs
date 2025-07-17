using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class LootableItem : NetworkBehaviour
{
    private readonly HashSet<GameObject> alreadyTriggered = new();
    private LootableItemState _state = LootableItemState.Available;

    [SerializeField]
    private Item _lootItem;

    [SerializeField]
    private int _lootQuantity = 1;

    public void Awake()
    {
        if (_lootItem == null)
        {
            Debug.LogError($"{gameObject.name}: LootItem is not assigned.");
            return;
        }

        if (_lootQuantity <= 0)
        {
            Debug.LogError($"{gameObject.name}: LootQuantity must be greater than zero.");
            _lootQuantity = 1;
        }
    }

    public Item GetItem()
    {
        return _lootItem;
    }

    public int GetQuantity()
    {
        return _lootQuantity;
    }

    [ServerRpc(RequireOwnership = false)]
    public void OnLootConfirmed()
    {
        _state = LootableItemState.Looted;
        _lootQuantity = 0;
        Despawn(DespawnType.Destroy);
    }

    [ServerRpc(RequireOwnership = false)]
    public void OnLootCancelled(int remaining)
    {
        _state = LootableItemState.Available;

        if (remaining == -1)
            return;

        _lootQuantity = remaining;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServerInitialized)
            return;

        if (!other.CompareTag(GameTags.Player))
            return;

        if (!alreadyTriggered.Add(other.gameObject))
            return;

        if (other.TryGetComponent<PlayerInventory>(out var playerInventory))
        {
            if (_state == LootableItemState.Available)
            {
                _state = LootableItemState.Pending;

                playerInventory.TryPickupLoot(playerInventory.Owner, NetworkObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsServerInitialized)
            return;

        alreadyTriggered.Remove(other.gameObject);
    }
}
