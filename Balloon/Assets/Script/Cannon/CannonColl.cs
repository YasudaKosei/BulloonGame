using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonColl : MonoBehaviour
{
    public CannonManager cannonManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            cannonManager.OnCannonBall();
        }
    }
}
