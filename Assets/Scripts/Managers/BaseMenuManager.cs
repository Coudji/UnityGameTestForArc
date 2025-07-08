using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public abstract class BaseMenuManager : NetworkBehaviour
{
    [SerializeField]
    private MenuName _menuName;
    protected PlayerInput _playerInput;
    public UIDocument MenuDocument;

    public override void OnStartClient()
    {
        base.OnStartClient();
        _playerInput = GetComponentInParent<PlayerInput>();
        HideCursor();
    }

    public void OpenMenu()
    {
        GameUIEvents.RequestOpen(_menuName);
        _playerInput.SwitchCurrentActionMap("UI");
        ShowCursor();
    }

    public void CloseMenu()
    {
        GameUIEvents.RequestClose(_menuName);
        _playerInput.SwitchCurrentActionMap("Player");
        HideCursor();
    }

    private void ShowCursor()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    private void HideCursor()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
}
