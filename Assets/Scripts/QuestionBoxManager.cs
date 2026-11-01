using UnityEngine;

public class QuestionBoxManager : MonoBehaviour
{
    public void ResetQuestionBoxes()
    {
        foreach (QuestionBoxBehaviour questionBox in GetComponentsInChildren<QuestionBoxBehaviour>(true))
        {
            questionBox.QuestionBoxReset();
        }

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(p);
        }
    }
}
