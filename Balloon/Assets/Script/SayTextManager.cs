using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SayTextManager : MonoBehaviour
{
    public Text text; // SayTextを表示するためのText
    public string[] sayText;
    public AudioClip[] audioClip;
    public AudioSource audioSource;

    public float fadeDuration = 1.0f; // フェードイン、フェードアウトの時間

    public void OnSayText(int sayTextNum, float delayDuration)
    {
        if (sayTextNum < 0 || sayTextNum >= sayText.Length || sayTextNum >= audioClip.Length)
        {
            Debug.LogError("Invalid sayTextNum index.");
            return;
        }

        StartCoroutine(DelayedSayTextCoroutine(sayTextNum, delayDuration));
    }

    private IEnumerator DelayedSayTextCoroutine(int sayTextNum, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(SayTextCoroutine(sayTextNum));
    }

    private IEnumerator SayTextCoroutine(int sayTextNum)
    {
        text.text = sayText[sayTextNum];
        audioSource.clip = audioClip[sayTextNum];
        audioSource.Play();

        yield return StartCoroutine(FadeTextIn());

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeTextOut());
    }

    private IEnumerator FadeTextIn()
    {
        float elapsedTime = 0f;
        Color color = text.color;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            text.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1f;
        text.color = color;
    }

    private IEnumerator FadeTextOut()
    {
        float elapsedTime = 0f;
        Color color = text.color;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            text.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0f;
        text.color = color;
    }
}
