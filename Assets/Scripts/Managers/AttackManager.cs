using FishNet.Object;
using UnityEngine;

public class AttackManager : BaseCharacterManager
{
    private CharacterStateManager _stateManager;
    private EquipmentManager _equipmentManager;
    private Animator _animator;

    public override void Start()
    {
        base.Start();
        _stateManager = _character.StateManager;
        _equipmentManager = _character.EquipmentManager;
        _animator = _character.Animator;
    }

    [ServerRpc]
    public void PerformAttack()
    {
        Debug.Log(
            $"[SERVER={IsServerInitialized}] [CLIENT={IsClientInitialized}] AttackManager started on object: {gameObject.name}"
        );

        if (_equipmentManager.HasWeapon() && _stateManager.CanAttack)
        {
            _animator.SetTrigger("Attack");
        }
    }
}
