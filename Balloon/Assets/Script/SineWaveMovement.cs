using UnityEngine;

public class SineWaveMovement : MonoBehaviour
{
    public float amplitude = 1.0f;  // 振幅（どれだけ上下するか）
    public float frequency = 1.0f;  // 周波数（どれくらい速く上下するか）

    private Vector3 startPosition;  // 開始位置

    void Start()
    {
        startPosition = transform.position;  // 開始時の位置を記録
    }

    void Update()
    {
        float y = amplitude * Mathf.Sin(Time.time * frequency * 2 * Mathf.PI);  // サインカーブの計算
        transform.position = startPosition + new Vector3(0, y, 0);  // 新しい位置を設定
    }
}
