using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Random.Range(0, 100) * Time.deltaTime, Random.Range(0, 100) * Time.deltaTime, Random.Range(0, 100) * Time.deltaTime, Space.World);
    }
}
