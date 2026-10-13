using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{ 
    [Header("References")]
    [SerializeField] AudioMixer mixer;

    [Header("SFX")]
    [SerializeField] AudioSource deathAudio;
    [SerializeField] AudioSource gameOverAudio; 

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
}
