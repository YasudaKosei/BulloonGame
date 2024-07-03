using UnityEngine;
using System.Collections;

public class PanelRotateShakeController : MonoBehaviour
{
    public RectTransform panel; // �h�炷�p�l��
    public float shakeAmount = 10.0f; // �h�炷�p�x
    public float shakeSpeed = 5.0f; // �h�炷���x

    private Quaternion originalRotation;

    private void Awake()
    {
        if (panel == null)
        {
            Debug.LogError("Panel is not assigned in the inspector.");
            return;
        }

        originalRotation = panel.localRotation;
    }

    private void OnEnable()
    {
        StartCoroutine(ShakePanel());
    }

    private IEnumerator ShakePanel()
    {
        while (true)
        {
            float angle = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            panel.localRotation = originalRotation * Quaternion.Euler(0, 0, angle);
            yield return null;
        }
    }
}


