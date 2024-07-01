using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SpeedChanger : MonoBehaviour
{
    //Balloonのスピード管理

    public ObjectController objectController;
    public FireManager fireManager;

    public float[] speedChangeVal;

    public void ChangeFloatSpeed(float value)
    {
        value = 1 - value;

        if (value <= speedChangeVal[0] || BalloonManager.isFalling || BalloonManager.wait)
        {
            BalloonManager.balloonFireLevel = 0;
            fireManager.SetFireScale();
            objectController.SetFloatSpeed();
        }
        else if (value <= speedChangeVal[1])
        {
            BalloonManager.balloonFireLevel = 1;
            fireManager.SetFireScale();
            objectController.SetFloatSpeed();
        }
        else if (value <= speedChangeVal[2])
        {
            BalloonManager.balloonFireLevel = 2;
            fireManager.SetFireScale();
            objectController.SetFloatSpeed();
        }
    }

    private void Update()
    { 
        if (BalloonManager.controllerNum != 0 || Gamepad.current == null) return;

        if(Gamepad.current.buttonEast.isPressed)
        {
            BalloonManager.balloonFireLevel = 1;
            fireManager.SetFireScale();
            objectController.SetFloatSpeed();
        }
        else if(Gamepad.current.buttonNorth.isPressed)
        {
            BalloonManager.balloonFireLevel = 2;
            fireManager.SetFireScale();
            objectController.SetFloatSpeed();
        }
        else
        {
            BalloonManager.balloonFireLevel = 0;
            fireManager.SetFireScale();
            objectController.SetFloatSpeed();
        }
    }
}
