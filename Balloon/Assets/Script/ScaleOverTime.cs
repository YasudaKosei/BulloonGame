using System.Collections;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public Vector3 startScale = Vector3.one;  // �J�n�X�P�[��
    public Vector3 endScale = Vector3.one * 2; // �I���X�P�[��
    public float duration = 1.0f;             // �ω��ɂ����鎞��

    private void OnEnable()
    {
        StartCoroutine(ScaleCoroutine());
    }

    private IEnumerator ScaleCoroutine()
    {
        float timeElapsed = 0f;

        // �����X�P�[����ݒ�
        transform.localScale = startScale;

        while (timeElapsed < duration)
        {
            // ���Ԃɑ΂���i�s�x���v�Z
            float t = timeElapsed / duration;

            // ���`��Ԃ��g�p���ăX�P�[�����v�Z
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            // �o�ߎ��Ԃ��X�V
            timeElapsed += Time.deltaTime;

            // 1�t���[���ҋ@
            yield return null;
        }

        // �I���X�P�[�����ŏI�I�ɐݒ�
        transform.localScale = endScale;
    }
}
