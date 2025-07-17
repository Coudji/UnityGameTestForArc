using FishNet.Object;
using UnityEngine;

[System.Serializable]
public class NetworkSocket
{
    public Transform Transform;
    public NetworkObject NetworkObject;
}

public class CharacterEquipment : NetworkBehaviour
{
    [Header("Sockets")]
    [SerializeField]
    private NetworkSocket _rightHandSocket;
    public Transform LeftHandSocket;
    public Transform HelmetSocket;

    [Header("Current Equipment")]
    private NetworkObject _currentWeapon;
    private GameObject _currentHelmet;

    public void OnEnable()
    {
        ItemEvents.OnItemEquipped += EquipItem;
    }

    public void OnDisable()
    {
        ItemEvents.OnItemEquipped -= EquipItem;
    }

    public void EquipItem(Item item)
    {
        switch (item.SubCategory)
        {
            case ItemSubCategory.Weapon:
                ServerEquipWeapon(item.Id);
                break;
        }
    }

    [ServerRpc]
    private void ServerEquipWeapon(int itemId)
    {
        if (_currentWeapon != null)
        {
            Despawn(_currentWeapon, DespawnType.Destroy);
            _currentWeapon = null;
        }

        Item item = ItemManager.GetItem(itemId);

        if (item == null || item.WorldModelPrefab == null)
        {
            Debug.LogError($"Item with ID {itemId} not found or has no world model prefab.");
            return;
        }

        NetworkObject weaponNob = Instantiate(item.WorldModelPrefab, _rightHandSocket.Transform);
        weaponNob.SetParent(_rightHandSocket.NetworkObject);
        Spawn(weaponNob);

        _currentWeapon = weaponNob;
    }

    public bool HasWeapon()
    {
        return _currentWeapon != null;
    }
}
