using System.Collections;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class CharacterStamina : NetworkBehaviour
{
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
    private bool _isSprinting;
    private MovementController _movementController;

    private void Awake()
    {
        _movementController = GetComponent<MovementController>();
    }

    public override void OnStartServer()
    {
        currentStamina.Value = _maxStamina;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner)
            return;

        currentStamina.OnChange += HandleStaminaChanged;
        CharacterEvents.OnSprintChanged += ServerHandleSprintChanged;

        // Force UI update on join
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
            StopCoroutine(_staminaCoroutine);
    }

    [ServerRpc]
    private void ServerHandleSprintChanged(bool isSprinting)
    {
        if (_isSprinting == isSprinting)
            return;

        _isSprinting = isSprinting;

        if (_staminaCoroutine != null)
            StopCoroutine(_staminaCoroutine);

        _staminaCoroutine = StartCoroutine(isSprinting ? UseStamina() : RegenerateStamina());
    }

    [TargetRpc]
    private void TargetLockSprint(NetworkConnection conn)
    {
        _movementController.LockSprint();
    }

    [TargetRpc]
    private void TargetUnlockSprint(NetworkConnection conn)
    {
        _movementController.UnlockSprint();
    }

    private void HandleStaminaChanged(float oldVal, float newVal, bool asServer)
    {
        float ratio = Mathf.Clamp01(newVal / _maxStamina);
        CharacterEvents.RaiseStaminaUpdated(ratio);
    }

    private IEnumerator UseStamina()
    {
        while (true)
        {
            currentStamina.Value = Mathf.Max(
                0f,
                currentStamina.Value - _drainPerSecond * _drainPeriod
            );

            if (currentStamina.Value <= 0f)
                TargetLockSprint(Owner);

            yield return new WaitForSeconds(_drainPeriod);
        }
    }

    private IEnumerator RegenerateStamina()
    {
        while (true)
        {
            yield return new WaitForSeconds(_regenPeriod);

            currentStamina.Value = Mathf.Min(
                _maxStamina,
                currentStamina.Value + _regenPerSecond * _regenPeriod
            );

            if (currentStamina.Value > 0f)
                TargetUnlockSprint(Owner);
        }
    }
}
