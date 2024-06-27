using UnityEngine;

public class EnamyManger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("跳ね返すときの速さ")]
    private float bounceSpeed = 30.0f;

    [SerializeField]
    [Tooltip("跳ね返す単位ベクトルにかける倍数")]
    private float bounceVectorMultiple = 3f;

    [SerializeField]
    [Tooltip("ダメージ")]
    private int damagee = 1;

    private ObjectController objectController;

    public GameObject particle;

    private void OnCollisionEnter(Collision collision)
    {
        // 当たった相手に"Player"タグが付いている場合
        if (collision.gameObject.tag == "Player")
        {
            BalloonManager.isFalling = true;

            objectController = collision.gameObject.GetComponent<ObjectController>();

            int hp = objectController.HP(damagee);

            if (hp < 1) return;

            // 衝突した面の、接触した点における法線ベクトルを取得
            Vector3 normal = collision.contacts[0].normal;
            // 衝突した速度ベクトルを単位ベクトルにする
            Vector3 velocity = collision.rigidbody.velocity.normalized;
            // x,z方向に対して逆向きの法線ベクトルを取得
            velocity += new Vector3(-normal.x * bounceVectorMultiple, 0f, -normal.z * bounceVectorMultiple);
            // 取得した法線ベクトルに跳ね返す速さをかけて、跳ね返す
            collision.rigidbody.AddForce(velocity * bounceSpeed, ForceMode.Impulse);

            // 接触した場所にパーティクルを生成する
            GameObject spawnParticle  = Instantiate(particle, collision.contacts[0].point, Quaternion.identity);

            Destroy(spawnParticle, 1.0f);

            BalloonManager.isFalling = false;
        }
    }
}
