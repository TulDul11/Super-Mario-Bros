using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioManager audioManager;

    void PlayJumpSound()
    {
        playerAudio.PlayOneShot(playerAudio.clip);
    }

    void PlayDeathSound()
    {
        audioManager.PlayDeathSound();
    }

    public void PlayGameOverSound()
    {
        audioManager.PlayGameOverSound();
    }
}
