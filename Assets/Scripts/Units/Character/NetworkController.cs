using FishNet.Object;
using UnityEngine;

public class NetworkController : NetworkBehaviour
{
    public AudioClip LandingAudioClip;
    public AudioClip[] FootstepAudioClips;

    [Range(0, 1)]
    public float FootstepAudioVolume = 0.5f;
    private CharacterController _controller;
    private PlayerStamina _stamina;
    private MovementInputs _input;

    private bool _wasSprinting = false;

    public override void OnStartClient()
    {
        if (!IsOwner)
            return;

        _controller = GetComponent<CharacterController>();
        _stamina = GetComponent<PlayerStamina>();
        _input = GetComponent<MovementInputs>();
    }

    void Update()
    {
        if (!IsOwner)
            return;

        bool isSprinting = _input.sprint;

        if (isSprinting != _wasSprinting)
        {
            _wasSprinting = isSprinting;
            _stamina.SetRunningState(isSprinting);
        }
    }

    public void OnFootstep(AnimationEvent animationEvent)
    {
        if (!IsOwner || _controller == null)
            return;

        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            ServerPlayFootstep(transform.TransformPoint(_controller.center));
        }
    }

    [ServerRpc]
    private void ServerPlayFootstep(Vector3 position)
    {
        RpcPlayFootstep(position);
    }

    [ObserversRpc]
    private void RpcPlayFootstep(Vector3 position)
    {
        if (FootstepAudioClips.Length > 0)
        {
            var index = Random.Range(0, FootstepAudioClips.Length);
            AudioSource.PlayClipAtPoint(FootstepAudioClips[index], position, FootstepAudioVolume);
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (!IsOwner || _controller == null)
            return;

        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            ServerPlayLanding(transform.TransformPoint(_controller.center));
        }
    }

    [ServerRpc]
    private void ServerPlayLanding(Vector3 position)
    {
        RpcPlayLanding(position);
    }

    [ObserversRpc]
    private void RpcPlayLanding(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(LandingAudioClip, position, FootstepAudioVolume);
    }
}
