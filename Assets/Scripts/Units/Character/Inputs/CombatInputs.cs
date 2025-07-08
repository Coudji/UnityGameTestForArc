using UnityEngine;
using UnityEngine.InputSystem;

public class CombatInputs : MonoBehaviour
{
    public AttackManager AttackManager;

    public void OnAttack(InputValue value)
    {
        AttackManager.ServerPerformAttack();
    }
}
