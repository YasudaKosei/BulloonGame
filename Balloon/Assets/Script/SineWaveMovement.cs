using UnityEngine;

public class SineWaveMovement : MonoBehaviour
{
    public float amplitude = 1.0f;  // �U���i�ǂꂾ���㉺���邩�j
    public float frequency = 1.0f;  // ���g���i�ǂꂭ�炢�����㉺���邩�j

    private Vector3 startPosition;  // �J�n�ʒu

    void Start()
    {
        startPosition = transform.position;  // �J�n���̈ʒu���L�^
    }

    void Update()
    {
        float y = amplitude * Mathf.Sin(Time.time * frequency * 2 * Mathf.PI);  // �T�C���J�[�u�̌v�Z
        transform.position = startPosition + new Vector3(0, y, 0);  // �V�����ʒu��ݒ�
    }
}
