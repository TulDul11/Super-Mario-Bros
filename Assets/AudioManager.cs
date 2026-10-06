using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{ 
    [Header("References")]
    [SerializeField] AudioMixer mixer;

    // Private
    AudioMixerSnapshot normal;
    AudioMixerSnapshot gameOver;

    void Awake()
    {
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
}
