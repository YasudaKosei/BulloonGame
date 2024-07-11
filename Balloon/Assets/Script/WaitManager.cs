using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WaitManager : MonoBehaviour
{
    public Text waitText;
    public CameraFollow cameraFollow;
    public SayTextManager sayTextManager;

    void Update()
    {
        if (Gamepad.current != null && BalloonManager.wait)
        {
            //ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½‚©
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                OffWait();
            }
        }

        if (Gamepad.current != null && BalloonManager.wait)
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
        sayTextManager.OnSayText(0, 1f);
        sayTextManager.OnSayText(1, 6f);
        sayTextManager.OnSayText(2, 11.7f);
        cameraFollow.ZoomOut();
        waitText.text = "";
    }
}
