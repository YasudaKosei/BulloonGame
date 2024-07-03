using UnityEngine;

public class CameraFOVAdjuster : MonoBehaviour
{
    private Camera mainCamera; // ���C���J�������A�T�C��

    // �X�s�[�h�Ǝ���p�̐ݒ�
    public int currentSpeed; // �I�u�W�F�N�g�̌��݂̃X�s�[�h�i0�`4�̒l�j
    public float[] fovValues = new float[5] { 60f, 65f, 70f, 75f, 80f }; // ����p�̒l
    public float transitionSpeed = 2.0f; // ����p�̐؂�ւ����x

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
