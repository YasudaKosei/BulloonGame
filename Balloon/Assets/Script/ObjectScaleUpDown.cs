using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaleUpDown : MonoBehaviour
{
    public int defScale;
    public int upScale;

    void Start()
    {
        StartCoroutine("ScaleUpDown");
    }

    IEnumerator ScaleUpDown()
    {
        while (true)
        {

            for (float i = defScale; i < defScale + upScale; i += 0.1f)
            {
                this.transform.localScale = new Vector3(i, i, i);
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(1.0f);

            for (float i = defScale + upScale; i > defScale; i -= 0.1f)
            {
                this.transform.localScale = new Vector3(i, i, i);
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
