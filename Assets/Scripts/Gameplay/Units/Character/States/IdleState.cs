using UnityEngine;

namespace Arc.Gameplay.Units.Character.States
{
    public class IdleState : ICharacterState
    {
        private readonly CharacterState _characterState;
    
        public bool CanMove => true;
        public bool CanAttack => true;
    
        public IdleState(CharacterState characterState)
        {
            _characterState = characterState;
        }
    
        public void Enter() { }
    
        public void Update() { }
    
        public void Exit() { }
    }
}
