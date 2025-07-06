using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComponentToggle : NetworkBehaviour
{
    private CharacterManager _character;
    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private MovementInputs _inputs;
    private ControllerManager _controller;

    [SerializeField]
    private GameObject Cameras;

    [SerializeField]
    private GameObject UserInterfaces;

    [SerializeField]
    private GameObject Menus;

    [SerializeField]
    private GameObject Managers;

    public void Awake()
    {
        _character = GetComponent<CharacterManager>();
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _inputs = GetComponent<MovementInputs>();
        _controller = GetComponent<ControllerManager>();

        Cameras.SetActive(false);
        UserInterfaces.SetActive(false);
        Menus.SetActive(false);
        Managers.SetActive(false);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (IsOwner)
        {
            _character.enabled = true;
            _characterController.enabled = true;
            _playerInput.enabled = true;
            _inputs.enabled = true;
            _controller.enabled = true;

            Cameras.SetActive(true);
            UserInterfaces.SetActive(true);
            Menus.SetActive(true);
            Managers.SetActive(true);
        }
    }
}
