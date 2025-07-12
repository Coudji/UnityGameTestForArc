using System;
using System.Diagnostics;

public static class CharacterEvents
{
    public static event Action<float> OnStaminaUpdated;
    public static event Action<bool> OnSprintChanged;

    public static void RaiseStaminaUpdated(float staminaRatio)
    {
        OnStaminaUpdated?.Invoke(staminaRatio);
    }

    public static void RaiseSprintChanged(bool isSprinting)
    {
        OnSprintChanged?.Invoke(isSprinting);
    }
}
