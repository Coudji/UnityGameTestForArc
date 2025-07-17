using FishNet.Managing.Scened;
using FishNet.Object;

namespace Arc.Gameplay.Networking
{
    public class LobbyPlayer : NetworkBehaviour
    {
        public static LobbyPlayer LocalPlayer;
    
        public override void OnStartClient()
        {
            base.OnStartClient();
            if (IsOwner)
                LocalPlayer = this;
        }
    
        [ServerRpc]
        public void ServerRequestStartGame()
        {
            if (!IsServerInitialized)
                return;
    
            if (Owner.IsHost)
            {
                SceneLoadData sld = new(SceneNames.Game) { ReplaceScenes = ReplaceOption.All };
                NetworkManager.SceneManager.LoadGlobalScenes(sld);
            }
        }
    }
}
