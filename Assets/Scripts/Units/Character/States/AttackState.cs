public class AttackState : ICharacterState
{
    private readonly CharacterState _characterState;
    private readonly MovementController _movementController;

    public bool CanMove => false;
    public bool CanAttack => false;

    public AttackState(MovementController movementController, CharacterState characterState)
    {
        _movementController = movementController;
        _characterState = characterState;
    }

    public void Enter()
    {
        // _movementController.LockMovement();
        CharacterEvents.RaiseActionPerformed(20);
    }

    public void Update() { }

    public void Exit()
    {
        // _movementController.UnlockMovement();
    }
}
