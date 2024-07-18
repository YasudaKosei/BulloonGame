using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float gravityForce = 10f; // �z�����ޗ͂̋���

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag != "Player") return;

        // ���W�b�h�{�f�B�����I�u�W�F�N�g���m�F
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // �u���b�N�z�[���̒��S�ւ̕������v�Z
            Vector3 direction = (transform.position - other.transform.position).normalized;

            // ���W�b�h�{�f�B�ɗ͂�������
            rb.AddForce(direction * gravityForce * Time.deltaTime);
        }
    }
}
