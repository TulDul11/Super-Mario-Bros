using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public void ResetBricks()
    {
        foreach (BrickBehaviour brick in GetComponentsInChildren<BrickBehaviour>(true))
        {
            brick.BrickReset();
        }
    }

    void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.gameRestart.RemoveListener(ResetBricks);
        }
        
    }

    void Start()
    {
        GameManager.instance.gameRestart.AddListener(ResetBricks);
    }
}
