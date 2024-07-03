using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float shaketime;

    [SerializeField]
    private float shakeMagnitude;

    private float shakeCount;

    public void Shake()
    {
        StartCoroutine(ShakeColl());
    }

    IEnumerator ShakeColl()
    {
        Vector3 initPos = transform.position;

        shakeCount = 0;

        while (shakeCount < shaketime)
        {
            float x = initPos.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = initPos.y + Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.position = new Vector3(x, y, initPos.z);

            shakeCount += Time.deltaTime;

            yield return null;
        }

        transform.position = initPos;
    }
}
