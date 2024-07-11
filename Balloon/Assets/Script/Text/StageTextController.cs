using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageTextController : MonoBehaviour
{
    public Text text;
    public GameObject stageTextSE;
    public string message;
    public float fadeDuration = 1.0f;
    public float displayDuration = 2.0f;

    private bool isFadingIn;
    private bool isFadingOut;
    private Color originalColor;

    private void Start()
    {
        originalColor = text.color;
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        text.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        ShowText(message);
    }

    public void ShowText(string message)
    {
        if (isFadingIn || isFadingOut) return;
        GameObject go = Instantiate(stageTextSE);
        Destroy(go, 3.0f);
        text.text = message;
        text.enabled = true;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        isFadingIn = true;
        isFadingOut = false;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration && isFadingIn)
        {
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        isFadingIn = false;
        yield return new WaitForSeconds(displayDuration);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        isFadingOut = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration && isFadingOut)
        {
            float alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        isFadingOut = false;
        text.enabled = false;
    }
}
