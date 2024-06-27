using System.Collections;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    Transform player;  // �v���C���[��Transform���i�[
    public float speed = 5f;  // �I�u�W�F�N�g�̈ړ����x
    public float homingTime = 3f;  // �ǔ����鎞�ԁi�b�j

    private bool isHoming = true;  // �ǔ������ǂ����̃t���O
    private float homingTimer;  // �ǔ����Ԃ��v������^�C�}�[

    [SerializeField]
    [Tooltip("�_���[�W")]
    private int damagee = 1;

    private ObjectController objectController;

    public GameObject particle;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        homingTimer = homingTime;
    }

    void Update()
    {
        if (isHoming)
        {
            // �v���C���[�̕����Ɍ�����
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(direction);

            // �ǔ����Ԃ�����������
            homingTimer -= Time.deltaTime;
            if (homingTimer <= 0)
            {
                isHoming = false;
            }
        }
        else
        {
            // ���݂̌����ɒ��i����
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            Destroy(gameObject, 10.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �������������"Player"�^�O���t���Ă���ꍇ
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("�_���[�W");
            objectController = collision.gameObject.GetComponent<ObjectController>();

            int hp = objectController.HP(damagee);

            if (hp < 1) return;

            // �ڐG�����ꏊ�Ƀp�[�e�B�N���𐶐�����
            GameObject spawnParticle = Instantiate(particle, collision.contacts[0].point, Quaternion.identity);

            Destroy(spawnParticle, 1.0f);

            Destroy(gameObject);
        }
    }
}
