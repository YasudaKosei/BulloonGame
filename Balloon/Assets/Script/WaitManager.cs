using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WaitManager : MonoBehaviour
{
    public Text waitText;

    void Update()
    {
        if (Gamepad.current != null)
        {
            //�{�^���������ꂽ��
            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                OffWait();
            }
        }

        if (Keyboard.current != null)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                OffWait();
            }
        }

    }

    void OffWait()
    {
        BalloonManager.wait = false;
        waitText.text = "";
    }
}
