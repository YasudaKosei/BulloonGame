using UnityEngine;

public class SineWaveMover : MonoBehaviour
{
    public float amplitude = 1f; // 振幅（左右の移動範囲の半径）
    public float frequency = 1f; // 周波数（サインカーブの速さ）
    public float startX; // 開始X座標
    public float endX; // 終了X座標
    private float initialY; // 初期Y座標
    private float initialZ; // 初期Z座標
    private float elapsedTime; // 経過時間

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
