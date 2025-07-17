using UnityEngine;

public class MainMenuManager : StaticInstance<MainMenuManager>
{
    private PlayerSaveManager _playerSaveManager;
    private MainMenuController _ui;

    [SerializeField]
    private int _maxLobbySize = 3;

    [SerializeField]
    private int _maxCount = 10;

    private void Start()
    {
        _playerSaveManager = GetComponent<PlayerSaveManager>();
        _ui = GetComponent<MainMenuController>();

        if (_playerSaveManager == null)
        {
            Debug.LogError("PlayerSaveManager not found!");
            return;
        }

        if (_ui == null)
        {
            Debug.LogError("MainMenuController not found!");
            return;
        }
    }

    private void OnEnable()
    {
        LobbyEvents.OnLobbyListReady += OnLobbyListReady;
    }

    private void OnDisable()
    {
        LobbyEvents.OnLobbyListReady -= OnLobbyListReady;
    }

    public void OnClick_ContinueGame()
    {
        _playerSaveManager.ContinueGame();
    }

    public void OnClick_LoadGame()
    {
        _playerSaveManager.OpenLoadGameMenu();
    }

    public void OnClick_NewGame()
    {
        _playerSaveManager.StartNewGame();
    }

    public void OnClick_Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnClick_GoBack()
    {
        if (LobbyManager.CurrentLobbyID != 0)
        {
            _ui.StartLeavingLobby();
            LobbyManager.LeaveLobbyFromMenu();
        }
        else
        {
            _ui.HideStartGamePanel();
        }
    }

    public void OnClick_CreatePublicLobby()
    {
        LobbyManager.CreateLobby(_maxLobbySize, false);
    }

    public void OnClick_CreatePrivateLobby()
    {
        LobbyManager.CreateLobby(_maxLobbySize);
    }

    public void OnClick_InviteFriend()
    {
        LobbyManager.OpenInviteFriendOverlay();
    }

    public void OnClick_JoinPublicLobby()
    {
        LobbyManager.RequestPublicLobbies(_maxCount);
    }

    public void OnClick_StartGame()
    {
        if (LobbyPlayer.LocalPlayer != null)
            LobbyPlayer.LocalPlayer.ServerRequestStartGame();
    }

    private void OnLobbySelected(LobbyInfo lobby)
    {
        _ui.HideLobbyListPanel();
        _ui.LobbyIsCreated();
        LobbyManager.JoinLobby(lobby.ID);
    }

    public void OnLobbyListReady(LobbyInfo[] lobbies)
    {
        _ui.ClearLobbyListContent();

        foreach (LobbyInfo lobby in lobbies)
        {
            _ui.CreateLobbyButton(lobby, OnLobbySelected);
        }

        _ui.LobbyListIsCreated();
    }
}
