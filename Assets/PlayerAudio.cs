using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioSource deathAudio;
    [SerializeField] AudioSource gameOverAudio;

    void PlayJumpSound()
    {
        playerAudio.PlayOneShot(playerAudio.clip);
    }

    void PlayDeathSound()
    {
        deathAudio.PlayOneShot(deathAudio.clip);
    }

    public void PlayGameOverSound()
    {
        gameOverAudio.PlayOneShot(gameOverAudio.clip);
    }
}
