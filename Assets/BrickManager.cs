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
}
