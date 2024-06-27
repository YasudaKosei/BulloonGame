using UnityEngine;

public class EnamyManger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���˕Ԃ��Ƃ��̑���")]
    private float bounceSpeed = 30.0f;

    [SerializeField]
    [Tooltip("���˕Ԃ��P�ʃx�N�g���ɂ�����{��")]
    private float bounceVectorMultiple = 3f;

    [SerializeField]
    [Tooltip("�_���[�W")]
    private int damagee = 1;

    private ObjectController objectController;

    public GameObject particle;

    private void OnCollisionEnter(Collision collision)
    {
        // �������������"Player"�^�O���t���Ă���ꍇ
        if (collision.gameObject.tag == "Player")
        {
            BalloonManager.isFalling = true;

            objectController = collision.gameObject.GetComponent<ObjectController>();

            int hp = objectController.HP(damagee);

            if (hp < 1) return;

            // �Փ˂����ʂ́A�ڐG�����_�ɂ�����@���x�N�g�����擾
            Vector3 normal = collision.contacts[0].normal;
            // �Փ˂������x�x�N�g����P�ʃx�N�g���ɂ���
            Vector3 velocity = collision.rigidbody.velocity.normalized;
            // x,z�����ɑ΂��ċt�����̖@���x�N�g�����擾
            velocity += new Vector3(-normal.x * bounceVectorMultiple, 0f, -normal.z * bounceVectorMultiple);
            // �擾�����@���x�N�g���ɒ��˕Ԃ������������āA���˕Ԃ�
            collision.rigidbody.AddForce(velocity * bounceSpeed, ForceMode.Impulse);

            // �ڐG�����ꏊ�Ƀp�[�e�B�N���𐶐�����
            GameObject spawnParticle  = Instantiate(particle, collision.contacts[0].point, Quaternion.identity);

            Destroy(spawnParticle, 1.0f);

            BalloonManager.isFalling = false;
        }
    }
}
