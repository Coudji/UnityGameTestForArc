using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

// public class CharacterState : NetworkBehaviour
// {
//     private Dictionary<CharacterStateName, ICharacterState> _states;
//     private ICharacterState _currentState;

//     private MovementController _movementController;

//     private void Awake()
//     {
//         _movementController = GetComponent<MovementController>();

//         _states = new Dictionary<CharacterStateName, ICharacterState>
//         {
//             { CharacterStateName.Idle, new IdleState(this) },
//             { CharacterStateName.Attack, new AttackState(_movementController, this) },
//         };
//         SwitchTo(CharacterStateName.Idle);
//     }

//     public void SwitchTo(CharacterStateName stateName)
//     {
//         Debug.Log($"Switching to state: {stateName}");
//         _currentState?.Exit();
//         _currentState = _states[stateName];
//         _currentState.Enter();
//     }

//     private void Update() => _currentState?.Update();

//     public bool CanMove => _currentState?.CanMove ?? false;
//     public bool CanAttack => _currentState?.CanAttack ?? false;

//     public void AnimationHasStarted(string animationName)
//     {
//         if (!IsServerStarted && !IsOwner)
//             return;

//         switch (animationName)
//         {
//             case AnimationNames.Attack:
//                 SwitchTo(CharacterStateName.Attack);
//                 break;
//             default:
//                 Debug.LogWarning($"Unhandled animation start: {animationName}");
//                 break;
//         }
//     }

//     public void AnimationHasEnded(string animationName)
//     {
//         if (!IsServerStarted && !IsOwner)
//             return;

//         switch (animationName)
//         {
//             case AnimationNames.Attack:
//                 SwitchTo(CharacterStateName.Idle);
//                 break;
//             default:
//                 Debug.LogWarning($"Unhandled animation end: {animationName}");
//                 break;
//         }
//     }
// }

public class CharacterState : NetworkBehaviour
{
    private MovementController _movementController;

    public bool CanAttack = true;
    public bool CanMove = true;

    private void Awake()
    {
        _movementController = GetComponent<MovementController>();
    }

    public void AnimationHasStarted(string animationName)
    {
        if (!IsServerStarted)
            return;

        switch (animationName)
        {
            case AnimationNames.Attack:
                CanAttack = false;
                CanMove = false;
                _movementController.TargetCanMoveChanged(Owner, false);
                CharacterEvents.RaiseActionPerformed(20);
                break;
            default:
                Debug.LogWarning($"Unhandled animation start: {animationName}");
                break;
        }
    }

    public void AnimationHasEnded(string animationName)
    {
        if (!IsServerStarted)
            return;

        switch (animationName)
        {
            case AnimationNames.Attack:
                CanAttack = true;
                CanMove = true;
                _movementController.TargetCanMoveChanged(Owner, true);
                CharacterEvents.RaiseActionEnded();
                break;
            default:
                Debug.LogWarning($"Unhandled animation end: {animationName}");
                break;
        }
    }
}
