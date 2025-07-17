using System.Collections.Generic;
using FishNet.Managing;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : Singleton<LobbyManager>
{
    [Header("Scene & Managers")]
    [SerializeField]
    private string _menuSceneName = "MainMenu";

    [SerializeField]
    private NetworkManager _networkManager;

    [SerializeField]
    private FishySteamworks.FishySteamworks _fishySteamworks;

    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> JoinRequest;
    protected Callback<LobbyEnter_t> LobbyEntered;
    protected Callback<LobbyMatchList_t> LobbyListReceived;

    public static ulong CurrentLobbyID { get; private set; }

    private void OnEnable()
    {
        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        LobbyListReceived = Callback<LobbyMatchList_t>.Create(OnLobbyListReceived);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(_menuSceneName);
    }

    public static void CreateLobby(int maxLobbySize, bool isPrivate = true)
    {
        if (IsInLobby())
        {
            Debug.LogWarning(
                "Already in a lobby. Please leave the current lobby before creating a new one."
            );
            return;
        }

        LobbyEvents.RaiseLobbyCreation();

        ELobbyType lobbyType = isPrivate
            ? ELobbyType.k_ELobbyTypePrivate
            : ELobbyType.k_ELobbyTypePublic;
        SteamMatchmaking.CreateLobby(lobbyType, maxLobbySize);
    }

    public static void RequestPublicLobbies(int maxCount)
    {
        if (IsInLobby())
        {
            Debug.LogWarning(
                "Already in a lobby. Please leave the current lobby before requesting lobbies."
            );
            return;
        }

        LobbyEvents.RaiseLobbySearch();

        SteamMatchmaking.AddRequestLobbyListDistanceFilter(
            ELobbyDistanceFilter.k_ELobbyDistanceFilterWorldwide
        );
        SteamMatchmaking.AddRequestLobbyListStringFilter(
            LobbyKeys.Owner,
            LobbyKeys.DefaultOwner,
            ELobbyComparison.k_ELobbyComparisonEqual
        );
        SteamMatchmaking.AddRequestLobbyListResultCountFilter(maxCount);
        SteamMatchmaking.RequestLobbyList();
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            Debug.LogWarning("Lobby creation failed: " + callback.m_eResult);
            LobbyEvents.RaiseLobbyCreationFailed();
            return;
        }

        CurrentLobbyID = callback.m_ulSteamIDLobby;

        CSteamID lobbyId = new(CurrentLobbyID);
        string clientAddress = SteamUser.GetSteamID().ToString();
        string playerName = SteamFriends.GetPersonaName();

        SetLobbyDataBatch(
            lobbyId,
            new Dictionary<string, string>
            {
                { LobbyKeys.HostAddress, clientAddress },
                { LobbyKeys.Name, $"{playerName}'s lobby" },
                { LobbyKeys.Owner, LobbyKeys.DefaultOwner },
            }
        );

        _fishySteamworks.SetClientAddress(clientAddress);
        _fishySteamworks.StartConnection(true);

        LobbyEvents.RaiseLobbyCreated();
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        CurrentLobbyID = callback.m_ulSteamIDLobby;

        _fishySteamworks.SetClientAddress(
            SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), LobbyKeys.HostAddress)
        );
        _fishySteamworks.StartConnection(false);

        LobbyEvents.RaiseLobbyEntered();
    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyListReceived(LobbyMatchList_t callback)
    {
        uint lobbyCount = callback.m_nLobbiesMatching;
        LobbyInfo[] lobbies = new LobbyInfo[lobbyCount];

        for (int i = 0; i < lobbyCount; i++)
        {
            CSteamID lobbyID = SteamMatchmaking.GetLobbyByIndex(i);
            string lobbyName = SteamMatchmaking.GetLobbyData(lobbyID, LobbyKeys.Name);
            lobbies[i] = new LobbyInfo { ID = lobbyID, Name = lobbyName };
        }

        LobbyEvents.RaiseLobbyListReady(lobbies);
    }

    public static void JoinLobby(CSteamID steamID)
    {
        if (IsInLobby())
        {
            Debug.LogWarning(
                "Already in a lobby. Please leave the current lobby before joining another."
            );
            return;
        }

        if (SteamMatchmaking.RequestLobbyData(steamID))
        {
            LobbyEvents.RaiseJoinLobby();
            SteamMatchmaking.JoinLobby(steamID);
        }
    }

    private static void LeaveLobby()
    {
        SteamMatchmaking.LeaveLobby(new CSteamID(CurrentLobbyID));
        CurrentLobbyID = 0;

        Instance._fishySteamworks.StopConnection(false);

        if (Instance._networkManager.IsServerStarted)
            Instance._fishySteamworks.StopConnection(true);
    }

    public static void LeaveLobbyFromMenu()
    {
        if (!IsInLobby())
        {
            Debug.LogWarning("No lobby is currently active.");
            return;
        }

        LeaveLobby();
        LobbyEvents.RaiseLobbyLeft();
    }

    public static void LeaveLobbyFromGame()
    {
        if (!IsInLobby())
        {
            Debug.LogWarning("No lobby is currently active.");
            return;
        }

        LeaveLobby();
    }

    public static void OpenInviteFriendOverlay()
    {
        if (!IsInLobby())
        {
            Debug.LogWarning("No lobby is currently active.");
            return;
        }

        SteamFriends.ActivateGameOverlayInviteDialog(new CSteamID(CurrentLobbyID));
    }

    public static bool IsInLobby()
    {
        return CurrentLobbyID != 0;
    }

    private void SetLobbyDataBatch(CSteamID lobbyId, Dictionary<string, string> data)
    {
        foreach (var pair in data)
        {
            SteamMatchmaking.SetLobbyData(lobbyId, pair.Key, pair.Value);
        }
    }
}
