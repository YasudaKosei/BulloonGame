using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public Text hpText;
   
    private void Update()
    {
        hpText.text = BalloonManager.hp.ToString();
    }
}
