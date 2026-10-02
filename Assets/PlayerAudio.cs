using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioSource marioAudio;
    [SerializeField] AudioClip marioDeath;
    [SerializeField] AudioClip gameOver;

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathSound()
    {
        marioAudio.PlayOneShot(marioDeath);
    }
    public void PlayGameOverSound()
    {
        marioAudio.PlayOneShot(gameOver);
    }
}
