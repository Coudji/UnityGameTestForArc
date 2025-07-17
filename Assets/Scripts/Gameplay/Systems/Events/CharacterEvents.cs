using System;
using System.Diagnostics;

public static class CharacterEvents
{
    public static event Action<float> OnStaminaUpdated;
    public static event Action<bool> OnSprintChanged;
    public static event Action<int> OnActionPerformed;
    public static event Action OnActionEnded;
    public static event Action<bool> OnCanAttackChanged;

    public static void RaiseStaminaUpdated(float staminaRatio)
    {
        OnStaminaUpdated?.Invoke(staminaRatio);
    }

    public static void RaiseSprintChanged(bool isSprinting)
    {
        OnSprintChanged?.Invoke(isSprinting);
    }

    public static void RaiseActionPerformed(int staminaCost)
    {
        OnActionPerformed?.Invoke(staminaCost);
    }

    public static void RaiseActionEnded()
    {
        OnActionEnded?.Invoke();
    }

    public static void RaiseCanAttackChanged(bool canAttack)
    {
        OnCanAttackChanged?.Invoke(canAttack);
    }
}
