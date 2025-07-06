using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip LandingAudioClip;
    public AudioClip[] FootstepAudioClips;

    [Range(0, 1)]
    public float FootstepAudioVolume = 0.5f;

    public void PlayFootstep(Vector3 position)
    {
        if (FootstepAudioClips.Length > 0)
        {
            var index = Random.Range(0, FootstepAudioClips.Length);
            AudioSource.PlayClipAtPoint(FootstepAudioClips[index], position, FootstepAudioVolume);
        }
    }

    public void PlayLanding(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(LandingAudioClip, position, FootstepAudioVolume);
    }
}
