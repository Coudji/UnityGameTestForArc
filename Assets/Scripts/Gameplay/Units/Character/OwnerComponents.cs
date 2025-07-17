using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Arc.Gameplay.Units.Character
{
    public class OwnerComponents : NetworkBehaviour
    {
        private CharacterState _characterState;
        private CharacterStamina _stamina;
        private CharacterAttack _attack;
        private CharacterEquipment _equipment;
    
        [Header("Containers")]
        [SerializeField]
        private GameObject Cameras;
    
        [SerializeField]
        private GameObject UserInterfaces;
    
        [SerializeField]
        private GameObject Menus;
    
        private void Awake()
        {
            _characterState = GetComponent<CharacterState>();
            _stamina = GetComponent<CharacterStamina>();
            _attack = GetComponent<CharacterAttack>();
            _equipment = GetComponent<CharacterEquipment>();
        }
    
        public override void OnStartServer()
        {
            base.OnStartServer();
            _characterState.enabled = true;
            _stamina.enabled = true;
            _attack.enabled = true;
            _equipment.enabled = true;
        }
    
        public override void OnStartClient()
        {
            base.OnStartClient();
    
            if (IsOwner)
            {
                _characterState.enabled = true;
                _stamina.enabled = true;
                _attack.enabled = true;
                _equipment.enabled = true;
    
                GetComponent<CharacterController>().enabled = true;
                GetComponent<PlayerInput>().enabled = true;
                GetComponent<MovementInputs>().enabled = true;
                GetComponent<MovementController>().enabled = true;
                GetComponent<CombatInputs>().enabled = true;
                GetComponent<UIInputs>().enabled = true;
    
                Cameras.SetActive(true);
                UserInterfaces.SetActive(true);
                Menus.SetActive(true);
            }
        }
    }
}
