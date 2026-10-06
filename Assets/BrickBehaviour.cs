using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameManager gameManager;

    [Header("Spring Physics")]
    [SerializeField] float bounceForce = 10f;

    [Header("Coin")]
    [SerializeField] bool hasCoin;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float coinImpulse = 8f;
    [SerializeField] Transform coinSpawnPoint;

    [Header("Audio")]
    [SerializeField] AudioSource brickAudio;
    [SerializeField] AudioClip coinSound;

    // Private
    Rigidbody2D brickBody;
    bool used = false;

    void SpawnCoin()
    {
        GameObject coin = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);
        coin.GetComponent<Rigidbody2D>().AddForce(Vector2.up * coinImpulse, ForceMode2D.Impulse);
        brickAudio.PlayOneShot(coinSound);
        gameManager.AddScore(50);
    }

    public void BrickReset()
    {
        used = false;
    }

    void Awake()
    {
        brickBody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        brickBody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        if (!used && hasCoin)
        {
            SpawnCoin();
            used = true;
        }
    }
}
