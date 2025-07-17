using FishNet.Object;
using UnityEngine;
using Arc.Core.Constants;

namespace Arc.Gameplay.Units.Character
{
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
                    CharacterEvents.RaiseActionPerformed(CharacterConstants.AttackStaminaCost);
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
}
