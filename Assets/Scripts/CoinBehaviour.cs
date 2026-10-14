using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [Header("Coin Behaviour")]
    [SerializeField] float lifetime = 1.5f;
    

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
