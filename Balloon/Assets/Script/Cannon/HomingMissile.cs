using System.Collections;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    Transform player;  // プレイヤーのTransformを格納
    public float speed = 5f;  // オブジェクトの移動速度
    public float homingTime = 3f;  // 追尾する時間（秒）

    private bool isHoming = true;  // 追尾中かどうかのフラグ
    private float homingTimer;  // 追尾時間を計測するタイマー

    [SerializeField]
    [Tooltip("ダメージ")]
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
            // プレイヤーの方向に向かう
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(direction);

            // 追尾時間を減少させる
            homingTimer -= Time.deltaTime;
            if (homingTimer <= 0)
            {
                isHoming = false;
            }
        }
        else
        {
            // 現在の向きに直進する
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            Destroy(gameObject, 10.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 当たった相手に"Player"タグが付いている場合
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("ダメージ");
            objectController = collision.gameObject.GetComponent<ObjectController>();

            int hp = objectController.HP(damagee);

            if (hp < 1) return;

            // 接触した場所にパーティクルを生成する
            GameObject spawnParticle = Instantiate(particle, collision.contacts[0].point, Quaternion.identity);

            Destroy(spawnParticle, 1.0f);

            Destroy(gameObject);
        }
    }
}
