using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFader : MonoBehaviour
{
    public Text uiText; // フェードイン・アウトさせるテキスト
    public float fadeDuration = 1.0f; // フェードイン・アウトの時間
    public float displayDuration = 2.0f; // テキストが完全に表示される時間

    private void Start()
    {
        if (uiText != null)
        {
            StartCoroutine(FadeInOutLoop());
        }
    }

    private IEnumerator FadeInOutLoop()
    {
        while (true)
        {
            yield return StartCoroutine(FadeIn());
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeOut());
            yield return new WaitForSeconds(displayDuration);
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        Color color = uiText.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            uiText.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        Color color = uiText.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
            uiText.color = color;
            yield return null;
        }
    }
}
