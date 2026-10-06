using UnityEngine;

public class QuestionBoxBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameManager gameManager;

    [Header("Animation")]
    [SerializeField] Animator boxAnimator;

    [Header("Spring Physics")]
    [SerializeField] float bounceForce = 10f;

    [Header("Coin")]
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float coinImpulse = 8f;
    [SerializeField] Transform coinSpawnPoint;

    [Header("Audio")]
    [SerializeField] AudioSource boxAudio;
    [SerializeField] AudioClip coinSound;

    // Private
    Rigidbody2D boxBody;
    bool used = false;

    void SpawnCoin()
    {
        GameObject coin = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);
        coin.GetComponent<Rigidbody2D>().AddForce(Vector2.up * coinImpulse, ForceMode2D.Impulse);
        boxAudio.PlayOneShot(coinSound);
        gameManager.AddScore(50);
    }

    public void QuestionBoxReset()
    {
        used = false;
        boxAnimator.SetTrigger("used");
    }

    void Awake()
    {
        boxBody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (used || !other.CompareTag("Player"))
        {
            return;
        }

        boxBody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        SpawnCoin();
        boxAnimator.SetTrigger("used");
        used = true;
    }
}
