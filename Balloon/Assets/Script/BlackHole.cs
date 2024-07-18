using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float gravityForce = 10f; // 吸い込む力の強さ

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag != "Player") return;

        // リジッドボディを持つオブジェクトを確認
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // ブラックホールの中心への方向を計算
            Vector3 direction = (transform.position - other.transform.position).normalized;

            // リジッドボディに力を加える
            rb.AddForce(direction * gravityForce * Time.deltaTime);
        }
    }
}
