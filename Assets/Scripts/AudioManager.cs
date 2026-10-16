using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{ 
    [Header("References")]
    [SerializeField] AudioMixer mixer;

    [Header("SFX")]
    [SerializeField] AudioSource deathAudio;
    [SerializeField] AudioSource gameOverAudio; 
    [SerializeField] AudioSource PowerUpAudio;

    // Private
    AudioMixerSnapshot normal;
    AudioMixerSnapshot gameOver;

    public override void Awake()
    {
        base.Awake();
        
        normal = mixer.FindSnapshot("Snapshot");
        gameOver = mixer.FindSnapshot("GameOver");
    }

    public void GameOver()
    {
        gameOver.TransitionTo(0f);
    }

    public void GameRestart()
    {
        normal.TransitionTo(0f);
    }

    public void PlayDeathSound()
    {
        deathAudio.Play();
    }

    public void PlayGameOverSound()
    {
        gameOverAudio.Play();
    }

    public void PlayPowerUpSound()
    {
        PowerUpAudio.Play();
    }

    public void PauseGame()
    {
        gameOver.TransitionTo(0f);
    }

    public void UnPauseGame()
    {
        normal.TransitionTo(0f);
    }
}
