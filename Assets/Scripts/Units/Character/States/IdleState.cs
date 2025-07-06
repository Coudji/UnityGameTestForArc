using UnityEngine;

public class IdleState : ICharacterState
{
    private readonly CharacterManager _character;
    private readonly CharacterStateManager _stateManager;

    public bool CanMove => true;
    public bool CanAttack => true;

    public IdleState(CharacterManager character, CharacterStateManager stateManager)
    {
        _character = character;
        _stateManager = stateManager;
    }

    public void Enter() { }

    public void Update() { }

    public void Exit() { }
}
