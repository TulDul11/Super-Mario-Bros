using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioSource playerAudio;

    void PlayJumpSound()
    {
        playerAudio.PlayOneShot(playerAudio.clip);
    }

    void PlayDeathSound()
    {
        AudioManager.instance.PlayDeathSound();
    }

    public void PlayGameOverSound()
    {
        AudioManager.instance.PlayGameOverSound();
    }

    void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.gameOver.RemoveListener(PlayGameOverSound);
        }
    }

    void Start()
    {
        GameManager.instance.gameOver.AddListener(PlayGameOverSound);
    }
}
