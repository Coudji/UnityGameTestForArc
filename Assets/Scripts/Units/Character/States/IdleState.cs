using UnityEngine;

public class IdleState : ICharacterState
{
    private readonly CharacterStateManager _stateManager;

    public bool CanMove => true;
    public bool CanAttack => true;

    public IdleState(CharacterStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public void Enter() { }

    public void Update() { }

    public void Exit() { }
}
