using Unity.VisualScripting;
using UnityEngine;

public class GoombaStomp : MonoBehaviour
{
    [Header("References")]
    [SerializeField] EnemyMovement goomba;
    [SerializeField] AudioSource stompSource;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        Rigidbody2D marioBody = other.GetComponent<Rigidbody2D>();

        if (marioBody.linearVelocity.y > 0)
        {
            return;
        }

        if (other.transform.position.y < goomba.transform.position.y + 0.5f)
        {
            return;
        }

        goomba.Stomped();
        other.GetComponent<PlayerMovement>().StompBounce();
        stompSource.PlayOneShot(stompSource.clip);
    }
}
