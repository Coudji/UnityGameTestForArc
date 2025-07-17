using System;

public static class LobbyEvents
{
    public static event Action OnLobbyCreation;
    public static event Action OnLobbySearch;
    public static event Action OnLobbyCreationFailed;
    public static event Action OnLobbyCreated;
    public static event Action OnLobbyEntered;
    public static event Action<LobbyInfo[]> OnLobbyListReady;
    public static event Action OnJoinLobby;
    public static event Action OnLobbyLeft;

    public static void RaiseLobbyCreation()
    {
        OnLobbyCreation?.Invoke();
    }

    public static void RaiseLobbySearch()
    {
        OnLobbySearch?.Invoke();
    }

    public static void RaiseLobbyCreationFailed()
    {
        OnLobbyCreationFailed?.Invoke();
    }

    public static void RaiseLobbyCreated()
    {
        OnLobbyCreated?.Invoke();
    }

    public static void RaiseLobbyEntered()
    {
        OnLobbyEntered?.Invoke();
    }

    public static void RaiseLobbyListReady(LobbyInfo[] lobbies)
    {
        OnLobbyListReady?.Invoke(lobbies);
    }

    public static void RaiseJoinLobby()
    {
        OnJoinLobby?.Invoke();
    }

    public static void RaiseLobbyLeft()
    {
        OnLobbyLeft?.Invoke();
    }
}
