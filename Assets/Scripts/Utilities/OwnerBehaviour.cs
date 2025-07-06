using FishNet.Object;

public abstract class OwnerBehaviour : NetworkBehaviour
{
    public override void OnStartClient()
    {
        enabled = IsOwner;
    }
}
