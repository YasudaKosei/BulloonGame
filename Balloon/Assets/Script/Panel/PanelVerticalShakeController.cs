using UnityEngine;
using System.Collections;

public class PanelVerticalShakeController : MonoBehaviour
{
    public RectTransform panel; // óhÇÁÇ∑ÉpÉlÉã
    public float shakeAmount = 10.0f; // óhÇÁÇ∑ïù
    public float shakeSpeed = 5.0f; // óhÇÁÇ∑ë¨ìx

    private Vector2 originalPosition;

    private void Awake()
    {
        if (panel == null)
        {
            Debug.LogError("Panel is not assigned in the inspector.");
            return;
        }

        originalPosition = panel.anchoredPosition;
    }

    private void OnEnable()
    {
        StartCoroutine(ShakePanel());
    }

    private IEnumerator ShakePanel()
    {
        while (true)
        {
            float y = originalPosition.y + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            panel.anchoredPosition = new Vector2(originalPosition.x, y);
            yield return null;
        }
    }
}
