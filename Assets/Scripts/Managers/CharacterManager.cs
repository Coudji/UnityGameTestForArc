using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterStateManager StateManager;
    public InputsManager InputsManager;
    public EquipmentManager EquipmentManager;
    public AttackManager AttackManager;
    public Animator Animator;
    public InventoryMenu InventoryMenu;

    [HideInInspector]
    public ControllerManager Controller;

    private void Awake()
    {
        StateManager.Init(this);
        InputsManager.Init(this);
        EquipmentManager.Init(this);
        AttackManager.Init(this);

        Controller = GetComponent<ControllerManager>();
    }

    public void AnimationHasStarted(string animationName)
    {
        StateManager.AnimationHasStarted(animationName);
    }

    public void AnimationHasEnded(string animationName)
    {
        StateManager.AnimationHasEnded(animationName);
    }
}
