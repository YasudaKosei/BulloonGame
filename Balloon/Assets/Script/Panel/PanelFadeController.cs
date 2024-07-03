using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelFadeController : MonoBehaviour
{
    public Image panelA;
    public Image panelB;
    public float fadeDuration = 1.0f;
    public float displayDuration = 2.0f;

    private void OnEnable()
    {
        StartCoroutine(FadePanels());
    }

    private IEnumerator FadePanels()
    {
        while (true)
        {
            // 初期状態設定
            SetPanelAlpha(panelA, 1.0f);
            SetPanelAlpha(panelB, 0.0f);

            yield return StartCoroutine(FadeIn(panelA));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeOut(panelA));

            // フェードアウト完了後の状態設定
            SetPanelAlpha(panelA, 0.0f);
            SetPanelAlpha(panelB, 1.0f);

            yield return StartCoroutine(FadeIn(panelB));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeOut(panelB));
        }
    }

    private IEnumerator FadeIn(Image panel)
    {
        float elapsedTime = 0.0f;
        Color color = panel.color;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            panel.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1.0f;
        panel.color = color;
    }

    private IEnumerator FadeOut(Image panel)
    {
        float elapsedTime = 0.0f;
        Color color = panel.color;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
            panel.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0.0f;
        panel.color = color;
    }

    private void SetPanelAlpha(Image panel, float alpha)
    {
        Color color = panel.color;
        color.a = alpha;
        panel.color = color;
    }
}
