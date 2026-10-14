using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneBehaviour : MonoBehaviour
{
    [SerializeField] CanvasGroup content;
    [SerializeField] Image fadeOverlay;
    [SerializeField] float contentFadeTime = 2f;
    [SerializeField] float overlayFadeTime = 1f;
    [SerializeField] string nextScene = "World-1-1";

    IEnumerator Sequence()
    {
        yield return StartCoroutine(FadeGroup(content, 1f, 0f, contentFadeTime));
        yield return StartCoroutine(FadeImage(fadeOverlay, 0f, 1f, overlayFadeTime));
        SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
    }

    IEnumerator FadeGroup(CanvasGroup g, float from, float to, float duration)
    {
        for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
        {
            g.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        g.alpha = to;
    }

    IEnumerator FadeImage(Image img, float to, float from, float duration)
    {
        Color c = img.color;
        for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
        {
            c.a = Mathf.Lerp(from, to, t / duration);
            img.color = c;
            yield return null;
        }
        c.a = to;
        img.color = c;
    }

    public void ReturnToMainMenu()
    {
        StopAllCoroutines();
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }

    void Start()
    {
        StartCoroutine(Sequence());
    }
}
