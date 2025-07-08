using FishNet.Object;

public class HUDController : NetworkBehaviour
{
    private HUDManager _hud;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner)
            return;

        _hud = HUDManager.Instance;

        _hud.Show();
        _hud.UpdateHealthBar(1.0f);

        // _health.OnHealthChanged += UpdateHealthHUD;
    }

    private void UpdateHealthHUD(int current, int max)
    {
        _hud.UpdateHealthBar(current / max);
    }
}
