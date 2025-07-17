using UnityEngine;
using UnityEngine.InputSystem;

namespace Arc.Gameplay.Units.Character.Inputs
{
    public class CombatInputs : MonoBehaviour
    {
        private CharacterAttack _characterAttack;
    
        private void Awake()
        {
            _characterAttack = GetComponent<CharacterAttack>();
        }
    
        public void OnAttack(InputValue value)
        {
            _characterAttack.ServerPerformAttack();
        }
    }
}
