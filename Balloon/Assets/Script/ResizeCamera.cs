using UnityEngine;

public class ResizeCamera : MonoBehaviour
{
    //ゲーム開始時のカメラの移動処理

    public Camera mainCamera;
    public float transitionDuration = 1.0f; // サイズ変更にかける秒数
    public float targetOrthographicSize = 5.0f; // 目標のカメラサイズ

    private float initialOrthographicSize; // 開始時のカメラサイズ
    private float transitionTimer = 0.0f; // 変更の経過時間
    private bool isTransitioning = true; // サイズ変更中かどうかのフラグ

    void Start()
    {
        initialOrthographicSize = mainCamera.orthographicSize;
    }

    void Update()
    {
        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionDuration); // 経過時間を0から1の範囲に制限

            // 補間関数を使用してカメラのサイズを変更
            float newSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, t);
            mainCamera.orthographicSize = newSize;

            // カメラのアスペクト比を考慮して左右方向のサイズを調整
            float aspectRatio = (float)Screen.width / Screen.height;
            float newWidth = newSize * aspectRatio;
            mainCamera.aspect = aspectRatio;

            // 目標のサイズに達したらサイズ変更を停止
            if (t >= 1.0f)
            {
                isTransitioning = false;
            }
        }
    }
}
