using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //カメラの追従

    public Transform target; // 追従するオブジェクト
    public float offsetY = 2f; // カメラのY座標のオフセット

    void Update()
    {
        // 追従するオブジェクトの高さを取得
        float targetHeight = target.position.y;

        // カメラの位置を更新
        Vector3 newPosition = transform.position;
        newPosition.y = targetHeight + offsetY;
        transform.position = newPosition;
    }
}
