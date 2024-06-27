using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public GameObject cannonBall;
    public Transform pos;

    // Update is called once per frame
    public void OnCannonBall()
    {
        Instantiate(cannonBall, pos.position, Quaternion.identity);
    }
}
