using UnityEngine;
using UnityEngine.UI;

public class SpeedChanger : MonoBehaviour
{
    //Balloonのスピード管理

    public ObjectController objectController;
    public FireManager fireManager;

    public float[] speedChangeVal;

    public void ChangeFloatSpeed(float value)
    {
        value = 1 - value;

        if (value <= speedChangeVal[0] || BalloonManager.isFalling)
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
        else if (value <= speedChangeVal[3])
        {
            BalloonManager.balloonFireLevel = 3;
            fireManager.SetFireScale();
            objectController.SetFloatSpeed();
        }
        else
        {
            BalloonManager.balloonFireLevel = 4;
            fireManager.SetFireScale();
            objectController.SetFloatSpeed();
        }
    }
}
