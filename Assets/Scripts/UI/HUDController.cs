using FishNet.Object;
using UnityEngine;

public class HUDController : NetworkBehaviour
{
    private HUDManager _hud;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner)
            return;

        _hud = HUDManager.Instance;

        if (_hud == null)
        {
            Debug.LogError(
                "HUDManager instance is not set. Make sure it is initialized before using HUDController."
            );
            return;
        }

        _hud.Show();
        _hud.UpdateHealthBar(1.0f);

        // _health.OnHealthChanged += UpdateHealthHUD;
    }

    private void UpdateHealthHUD(int current, int max)
    {
        _hud.UpdateHealthBar(current / max);
    }
}
