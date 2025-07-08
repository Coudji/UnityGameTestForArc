using FishNet.Object;
using UnityEngine;

public class EquipmentManager : NetworkBehaviour
{
    [Header("Sockets")]
    public Transform RightHandSocket;
    public Transform LeftHandSocket;
    public Transform HelmetSocket;

    [Header("Current Equipment")]
    private GameObject _currentWeapon;
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
                EquipWeaponServerRpc(item.Id);
                break;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void EquipWeaponServerRpc(int itemId)
    {
        if (_currentWeapon != null)
            Despawn(_currentWeapon, DespawnType.Destroy);

        Item item = ItemManager.GetItem(itemId);

        if (item.WorldModelPrefab != null)
        {
            GameObject weapon = Instantiate(item.WorldModelPrefab, RightHandSocket);
            Spawn(weapon);
            ObserversSetWeaponVisual(weapon);
            _currentWeapon = weapon;
        }
    }

    [ObserversRpc]
    private void ObserversSetWeaponVisual(GameObject weapon)
    {
        weapon.transform.SetPositionAndRotation(RightHandSocket.position, RightHandSocket.rotation);
        weapon.transform.parent = RightHandSocket.transform;
    }

    public bool HasWeapon()
    {
        return _currentWeapon != null;
    }
}
