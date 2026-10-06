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
}
