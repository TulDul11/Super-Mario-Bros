using UnityEngine;

public class QuestionBoxBehaviour : MonoBehaviour
{
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
    [SerializeField] AudioClip powerUpSound;

    [Header("Mushrooms")]
    [SerializeField] bool spawnsMushroom;
    [SerializeField] GameObject mushroomPrefab;

    // Private
    Rigidbody2D boxBody;
    bool used = false;

    void SpawnCoin()
    {
        GameObject coin = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);
        coin.GetComponent<Rigidbody2D>().AddForce(Vector2.up * coinImpulse, ForceMode2D.Impulse);
        boxAudio.PlayOneShot(coinSound);
        GameManager.instance.AddScore(50);
    }

    void SpawnMushroom()
    {
        GameObject m = Instantiate(mushroomPrefab, coinSpawnPoint.position, Quaternion.identity);
        m.GetComponent<MagicMushroomPowerUp>().SpawnPowerUp();
        boxAudio.PlayOneShot(powerUpSound);
    }

    public void QuestionBoxReset()
    {
        if (used)
        {
            boxAnimator.SetTrigger("used");
        }
        used = false;
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

        if (spawnsMushroom)
            SpawnMushroom();
        else
            SpawnCoin();

        boxAnimator.SetTrigger("used");
        used = true;
    }
}
