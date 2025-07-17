using FishNet.Object;
using UnityEngine;

public class CharacterAttack : NetworkBehaviour
{
    private CharacterState _characterState;
    private CharacterEquipment _characterEquipment;
    private Animator _animator;
    private bool _canAttack = true;

    private void Awake()
    {
        _characterState = GetComponent<CharacterState>();
        _characterEquipment = GetComponent<CharacterEquipment>();
        _animator = GetComponent<Animator>();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        CharacterEvents.OnCanAttackChanged += UpdateCanAttack;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        CharacterEvents.OnCanAttackChanged -= UpdateCanAttack;
    }

    [ServerRpc]
    public void ServerPerformAttack()
    {
        if (_characterEquipment.HasWeapon() && _characterState.CanAttack && _canAttack)
        {
            ObserversPerformAttack();
        }
    }

    [ObserversRpc]
    private void ObserversPerformAttack()
    {
        _animator.SetTrigger("Attack");
    }

    private void UpdateCanAttack(bool canAttack)
    {
        Debug.Log($"CanAttack updated: {canAttack}");
        _canAttack = canAttack;
    }
}
