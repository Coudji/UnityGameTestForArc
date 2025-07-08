using UnityEngine;

public class AttackState : ICharacterState
{
    private readonly CharacterStateManager _stateManager;
    private readonly MovementController _movementController;

    public bool CanMove => false;
    public bool CanAttack => false;

    public AttackState(MovementController movementController, CharacterStateManager stateManager)
    {
        _movementController = movementController;
        _stateManager = stateManager;
    }

    public void Enter()
    {
        _movementController.LockMovement();
    }

    public void Update() { }

    public void Exit()
    {
        _movementController.UnlockMovement();
    }
}
