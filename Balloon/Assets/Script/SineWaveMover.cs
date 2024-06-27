using UnityEngine;

public class SineWaveMover : MonoBehaviour
{
    public float amplitude = 1f; // �U���i���E�̈ړ��͈͂̔��a�j
    public float frequency = 1f; // ���g���i�T�C���J�[�u�̑����j
    public float startX; // �J�nX���W
    public float endX; // �I��X���W
    private float initialY; // ����Y���W
    private float initialZ; // ����Z���W
    private float elapsedTime; // �o�ߎ���

    void Start()
    {
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float x = Mathf.Lerp(startX, endX, (Mathf.Sin(elapsedTime * frequency) + 1f) / 2f);
        transform.position = new Vector3(x, initialY, initialZ);
    }
}
