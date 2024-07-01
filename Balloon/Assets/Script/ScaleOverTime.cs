using System.Collections;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public Vector3 startScale = Vector3.one;  // 開始スケール
    public Vector3 endScale = Vector3.one * 2; // 終了スケール
    public float duration = 1.0f;             // 変化にかかる時間

    private void OnEnable()
    {
        StartCoroutine(ScaleCoroutine());
    }

    private IEnumerator ScaleCoroutine()
    {
        float timeElapsed = 0f;

        // 初期スケールを設定
        transform.localScale = startScale;

        while (timeElapsed < duration)
        {
            // 時間に対する進行度を計算
            float t = timeElapsed / duration;

            // 線形補間を使用してスケールを計算
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            // 経過時間を更新
            timeElapsed += Time.deltaTime;

            // 1フレーム待機
            yield return null;
        }

        // 終了スケールを最終的に設定
        transform.localScale = endScale;
    }
}
