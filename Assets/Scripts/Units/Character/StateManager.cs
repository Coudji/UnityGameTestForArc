using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : BaseCharacterManager
{
    private Dictionary<CharacterStateName, ICharacterState> _states;
    private ICharacterState _currentState;

    public override void Init(CharacterManager character)
    {
        base.Init(character);

        _states = new Dictionary<CharacterStateName, ICharacterState>
        {
            { CharacterStateName.Idle, new IdleState(character, this) },
            { CharacterStateName.Attack, new AttackState(character, this) },
        };
        SwitchTo(CharacterStateName.Idle);
    }

    public void SwitchTo(CharacterStateName stateName)
    {
        Debug.Log($"Switching to state: {stateName}");
        _currentState?.Exit();
        _currentState = _states[stateName];
        _currentState.Enter();
    }

    private void Update() => _currentState?.Update();

    public bool CanMove => _currentState?.CanMove ?? false;
    public bool CanAttack => _currentState?.CanAttack ?? false;

    public void AnimationHasStarted(string animationName)
    {
        switch (animationName)
        {
            case AnimationNames.Attack:
                SwitchTo(CharacterStateName.Attack);
                break;
            default:
                Debug.LogWarning($"Unhandled animation start: {animationName}");
                break;
        }
    }

    public void AnimationHasEnded(string animationName)
    {
        switch (animationName)
        {
            case AnimationNames.Attack:
                SwitchTo(CharacterStateName.Idle);
                break;
            default:
                Debug.LogWarning($"Unhandled animation end: {animationName}");
                break;
        }
    }
}
