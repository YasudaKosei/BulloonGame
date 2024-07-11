using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CameraFollow : MonoBehaviour
{
    // カメラの追従
    public Transform target; // 追従するオブジェクト
    public float offsetY = 0f; // カメラのY座標のオフセット
    public float shakeDuration = 0.5f; // 揺れの持続時間
    public float shakeMagnitude = 0.1f; // 揺れの大きさ

    public float zoomDuration = 1.0f; // ズームアウトの持続時間
    public float targetFOV = 60f; // ズームアウト後のFOV

    private Vector3 originalPosition;
    private bool isShaking = false;
    private Camera cameraComponent;

    void Start()
    {
        cameraComponent = GetComponent<Camera>();
    }

    void Update()
    {
        // 追従するオブジェクトの高さを取得
        float targetHeight = target.position.y;
        float targetWide = target.position.x;

        // カメラの位置を更新
        Vector3 newPosition = new Vector3(targetWide, targetHeight + offsetY, transform.position.z);
        originalPosition = newPosition;

        if (BalloonManager.isFalling && !isShaking)
        {
            StartCoroutine(ShakeCamera());
        }
        else
        {
            transform.position = newPosition;
        }
    }

    IEnumerator ShakeCamera()
    {
        isShaking = true;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            if (BalloonManager.isFalling)
            {
                float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
                float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);

                transform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

                elapsed += Time.deltaTime;
            }

            yield return null;
        }

        isShaking = false;
    }

    public void ZoomOut()
    {
        StartCoroutine(ZoomOutColl());
    }

    IEnumerator ZoomOutColl()
    {
        float elapsed = 0.0f;
        float startFOV = cameraComponent.fieldOfView;

        while (elapsed < zoomDuration)
        {
            cameraComponent.fieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsed / zoomDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraComponent.fieldOfView = targetFOV;
    }
}

