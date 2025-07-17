using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;

public class ServerConnection : MonoBehaviour
{
    // #region Types.
    // /// <summary>
    // /// Ways the HUD will automatically start a connection.
    // /// </summary>
    // private enum AutoStartType
    // {
    //     Disabled,
    //     Host,
    //     Server,
    //     Client,
    // }
    // #endregion

    // #region Serialized.
    // /// <summary>
    // /// What connections to automatically start on play.
    // /// </summary>
    // [Tooltip("What connections to automatically start on play.")]
    // [SerializeField]
    // private AutoStartType _autoStartType = AutoStartType.Disabled;
    // #endregion

    // #region Private.
    // /// <summary>
    // /// Found NetworkManager.
    // /// </summary>
    // private NetworkManager _networkManager;

    // /// <summary>
    // /// Current state of client socket.
    // /// </summary>
    // private LocalConnectionState _clientState = LocalConnectionState.Stopped;

    // /// <summary>
    // /// Current state of server socket.
    // /// </summary>
    // private LocalConnectionState _serverState = LocalConnectionState.Stopped;

    // #endregion

    // private void Start()
    // {
    //     _networkManager = GetComponent<NetworkManager>();
    //     if (_networkManager == null)
    //     {
    //         Debug.LogError("NetworkManager not found, HUD will not function.");
    //         return;
    //     }
    //     else
    //     {
    //         _networkManager.ServerManager.OnServerConnectionState +=
    //             ServerManager_OnServerConnectionState;
    //         _networkManager.ClientManager.OnClientConnectionState +=
    //             ClientManager_OnClientConnectionState;
    //     }

    //     if (_autoStartType == AutoStartType.Host || _autoStartType == AutoStartType.Server)
    //         OnClick_Server();
    //     if (
    //         !Application.isBatchMode
    //         && (_autoStartType == AutoStartType.Host || _autoStartType == AutoStartType.Client)
    //     )
    //         OnClick_Client();
    // }

    // private void OnDestroy()
    // {
    //     if (_networkManager == null)
    //         return;

    //     _networkManager.ServerManager.OnServerConnectionState -=
    //         ServerManager_OnServerConnectionState;
    //     _networkManager.ClientManager.OnClientConnectionState -=
    //         ClientManager_OnClientConnectionState;
    // }

    // private void ClientManager_OnClientConnectionState(ClientConnectionStateArgs obj)
    // {
    //     _clientState = obj.ConnectionState;
    // }

    // private void ServerManager_OnServerConnectionState(ServerConnectionStateArgs obj)
    // {
    //     _serverState = obj.ConnectionState;
    // }

    // public void OnClick_Server()
    // {
    //     if (_networkManager == null)
    //         return;

    //     if (_serverState != LocalConnectionState.Stopped)
    //         _networkManager.ServerManager.StopConnection(true);
    //     else
    //         _networkManager.ServerManager.StartConnection();
    // }

    // public void OnClick_Client()
    // {
    //     if (_networkManager == null)
    //         return;

    //     if (_clientState != LocalConnectionState.Stopped)
    //         _networkManager.ClientManager.StopConnection();
    //     else
    //         _networkManager.ClientManager.StartConnection();
    // }
}
