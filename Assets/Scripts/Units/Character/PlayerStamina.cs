using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class PlayerStamina : OwnerBehaviour
{
    [SerializeField]
    public float maxStamina = 100f;

    [SerializeField]
    private float drainRate = 20f;

    [SerializeField]
    private float regenRate = 10f;

    public readonly SyncVar<float> currentStamina = new();

    public bool IsRunning { get; private set; }

    public event System.Action<float> OnStaminaUpdated;

    public override void OnStartServer()
    {
        currentStamina.Value = maxStamina;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        currentStamina.OnChange += HandleStaminaChanged;
    }

    private void HandleStaminaChanged(float oldVal, float newVal, bool asServer)
    {
        OnStaminaUpdated?.Invoke(newVal);
    }

    private void Update()
    {
        if (!IsServerInitialized)
            return;

        if (IsRunning)
            currentStamina.Value -= drainRate * Time.deltaTime;
        else
            currentStamina.Value += regenRate * Time.deltaTime;

        currentStamina.Value = Mathf.Clamp(currentStamina.Value, 0f, maxStamina);
    }

    [ServerRpc]
    public void SetRunningState(bool running)
    {
        if (currentStamina.Value <= 0)
        {
            IsRunning = false;
            return;
        }

        IsRunning = running;
    }
}
