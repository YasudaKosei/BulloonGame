using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSelect : MonoBehaviour
{
    public void SelectController(int controllerNum)
    {
        PlayerPrefs.SetInt("ControllerNum", controllerNum);
    }
}
