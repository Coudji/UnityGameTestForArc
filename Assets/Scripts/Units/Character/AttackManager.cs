using FishNet.Component.Animating;
using FishNet.Object;
using UnityEngine;

public class AttackManager : NetworkBehaviour
{
    [SerializeField]
    private CharacterStateManager _stateManager;

    [SerializeField]
    private EquipmentManager _equipmentManager;

    [SerializeField]
    private Animator _animator;

    [ServerRpc]
    public void ServerPerformAttack()
    {
        if (_equipmentManager.HasWeapon() && _stateManager.CanAttack)
        {
            ObserversPerformAttack();
        }
    }

    [ObserversRpc]
    private void ObserversPerformAttack()
    {
        _animator.SetTrigger("Attack");
    }
}
