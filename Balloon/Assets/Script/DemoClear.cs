using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoClear : MonoBehaviour
{
    public SceneLodeManager sceneLodeManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sceneLodeManager.LoadNextScene("DEMOEND");
        }
    }
}
