using UnityEngine;

public class AttackState : ICharacterState
{
    private readonly CharacterManager _character;
    private readonly CharacterStateManager _stateManager;

    public bool CanMove => false;
    public bool CanAttack => false;

    public AttackState(CharacterManager character, CharacterStateManager stateManager)
    {
        _character = character;
        _stateManager = stateManager;
    }

    public void Enter()
    {
        _character.Controller.LockMovement();
    }

    public void Update() { }

    public void Exit()
    {
        _character.Controller.UnlockMovement();
    }
}
