using UnityEngine;

public class QuestionBoxManager : MonoBehaviour
{
    public void ResetQuestionBoxes()
    {
        foreach (QuestionBoxBehaviour questionBox in GetComponentsInChildren<QuestionBoxBehaviour>(true))
        {
            questionBox.QuestionBoxReset();
        }
    }

    void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.gameRestart.RemoveListener(ResetQuestionBoxes);
        }
        
    }

    void Start()
    {
        GameManager.instance.gameRestart.AddListener(ResetQuestionBoxes);
    }
}
