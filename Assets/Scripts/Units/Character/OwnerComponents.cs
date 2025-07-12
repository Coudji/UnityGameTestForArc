using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class OwnerComponents : NetworkBehaviour
{
    private CharacterStateManager _stateManager;
    private CharacterStamina _stamina;

    [Header("Containers")]
    [SerializeField]
    private GameObject Cameras;

    [SerializeField]
    private GameObject UserInterfaces;

    [SerializeField]
    private GameObject Menus;

    [SerializeField]
    private GameObject Managers;

    private void Awake()
    {
        _stateManager = GetComponent<CharacterStateManager>();
        _stamina = GetComponent<CharacterStamina>();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        _stateManager.enabled = true;
        _stamina.enabled = true;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (IsOwner)
        {
            _stateManager.enabled = true;
            _stamina.enabled = true;

            GetComponent<CharacterController>().enabled = true;
            GetComponent<PlayerInput>().enabled = true;
            GetComponent<MovementInputs>().enabled = true;
            GetComponent<MovementController>().enabled = true;
            GetComponent<CombatInputs>().enabled = true;
            GetComponent<UIInputs>().enabled = true;

            Cameras.SetActive(true);
            UserInterfaces.SetActive(true);
            Menus.SetActive(true);
            Managers.SetActive(true);
        }
    }
}
