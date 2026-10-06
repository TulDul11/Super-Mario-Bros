using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public void GameRestart()
    {
        foreach (Transform eachChild in transform)
        {
            eachChild.GetComponent<EnemyMovement>().ResetGoomba();
        }
    }
}
