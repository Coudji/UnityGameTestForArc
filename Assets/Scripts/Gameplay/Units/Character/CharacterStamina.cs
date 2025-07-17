using System.Collections;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace Arc.Gameplay.Units.Character
{
    public class CharacterStamina : NetworkBehaviour
    {
        private MovementController _movementController;
    
        [Header("Stamina")]
        [SerializeField]
        private float _maxStamina;
        public readonly SyncVar<float> currentStamina = new();
    
        [Header("Stamina Drain")]
        [SerializeField]
        private float _drainPerSecond;
    
        [SerializeField]
        private float _drainPeriod;
    
        [Header("Stamina Regen")]
        [SerializeField]
        private float _regenPerSecond;
    
        [SerializeField]
        private float _regenPeriod;
    
        public event System.Action<float> OnStaminaUpdated;
    
        private Coroutine _staminaCoroutine;
    
        private bool _isSprinting = false;
        private bool _isStaminaBlocked = false;
    
        private void Awake()
        {
            _movementController = GetComponent<MovementController>();
        }
    
        public override void OnStartServer()
        {
            currentStamina.Value = _maxStamina;
    
            currentStamina.OnChange += HandleStaminaChanged;
            CharacterEvents.OnActionPerformed += HandleActionStaminaCost;
            CharacterEvents.OnActionEnded += UnlockStamina;
        }
    
        public override void OnStartClient()
        {
            base.OnStartClient();
    
            if (!IsOwner)
                return;
    
            currentStamina.OnChange += HandleStaminaChanged;
            CharacterEvents.OnSprintChanged += ServerHandleSprintChanged;
    
            HandleStaminaChanged(currentStamina.Value, currentStamina.Value, false);
        }
    
        public override void OnStopClient()
        {
            base.OnStopClient();
    
            if (!IsOwner)
                return;
    
            currentStamina.OnChange -= HandleStaminaChanged;
            CharacterEvents.OnSprintChanged -= ServerHandleSprintChanged;
        }
    
        public override void OnStopServer()
        {
            if (_staminaCoroutine != null)
            {
                StopCoroutine(_staminaCoroutine);
                _staminaCoroutine = null;
            }
    
            currentStamina.OnChange -= HandleStaminaChanged;
            CharacterEvents.OnActionPerformed -= HandleActionStaminaCost;
            CharacterEvents.OnActionEnded -= UnlockStamina;
        }
    
        [ServerRpc]
        private void ServerHandleSprintChanged(bool isSprinting)
        {
            if (_isSprinting == isSprinting)
                return;
    
            _isSprinting = isSprinting;
    
            RestartStaminaRoutine();
        }
    
        private void RestartStaminaRoutine()
        {
            if (_staminaCoroutine != null)
                StopCoroutine(_staminaCoroutine);
    
            _staminaCoroutine = StartCoroutine(_isSprinting ? UseStamina() : RegenerateStamina());
        }
    
        private void HandleStaminaChanged(float oldVal, float newVal, bool asServer)
        {
            if (asServer)
            {
                bool wasExhausted = oldVal <= 0f;
                bool isExhausted = newVal <= 0f;
    
                if (wasExhausted != isExhausted)
                {
                    bool canAct = !isExhausted;
    
                    _movementController.TargetCanSprintChanged(Owner, canAct);
                    CharacterEvents.RaiseCanAttackChanged(canAct);
                }
            }
            else
            {
                float ratio = Mathf.Clamp01(newVal / _maxStamina);
                CharacterEvents.RaiseStaminaUpdated(ratio);
            }
        }
    
        private void HandleActionStaminaCost(int staminaCost)
        {
            currentStamina.Value -= staminaCost;
            LockStamina();
            RestartStaminaRoutine();
        }
    
        private void LockStamina()
        {
            _isStaminaBlocked = true;
        }
    
        private void UnlockStamina()
        {
            _isStaminaBlocked = false;
        }
    
        private IEnumerator UseStamina()
        {
            float drainAmount = _drainPerSecond * _drainPeriod;
    
            while (true)
            {
                currentStamina.Value = Mathf.Max(0f, currentStamina.Value - drainAmount);
    
                yield return new WaitForSeconds(_drainPeriod);
            }
        }
    
        private IEnumerator RegenerateStamina()
        {
            float regenAmount = _regenPerSecond * _regenPeriod;
    
            while (true)
            {
                while (_isStaminaBlocked)
                    yield return null;
    
                yield return new WaitForSeconds(_regenPeriod);
    
                currentStamina.Value = Mathf.Min(_maxStamina, currentStamina.Value + regenAmount);
            }
        }
    }
}
