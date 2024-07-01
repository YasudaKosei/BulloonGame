using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        BalloonManager.controllerNum = PlayerPrefs.GetInt("ControllerNum", 0);
    }

}
