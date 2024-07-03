using UnityEngine;

public class CameraFOVAdjuster : MonoBehaviour
{
    private Camera mainCamera; // メインカメラをアサイン

    // スピードと視野角の設定
    public int currentSpeed; // オブジェクトの現在のスピード（0～4の値）
    public float[] fovValues = new float[5] { 60f, 65f, 70f, 75f, 80f }; // 視野角の値
    public float transitionSpeed = 2.0f; // 視野角の切り替え速度

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (BalloonManager.isFalling || BalloonManager.wait) return;

        AdjustFOV();
    }

    void AdjustFOV()
    {
        currentSpeed = BalloonManager.balloonFireLevel;

        if (currentSpeed >= 0 && currentSpeed < fovValues.Length)
        {
            float targetFOV = fovValues[currentSpeed];
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * transitionSpeed);
        }
    }
}
