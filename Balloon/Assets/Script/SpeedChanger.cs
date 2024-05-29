using UnityEngine;
using UnityEngine.UI;

public class SpeedChanger : MonoBehaviour
{
    //Balloonのスピード管理

    public ObjectController objectController;
    public FireManager fireManager;

    public void ChangeFloatSpeed(float value)
    {
        value = 1 - value;

        if (value <= 0.0 && !objectController.ObjectMove)
        {
            objectController.SetFloatSpeed(0);
            objectController.EnableGravity(true);
            fireManager.SetFireScale(value);
        }
        else if (value <= 0.1)
        {
            objectController.SetFloatSpeed(400);
            objectController.EnableGravity(false);
            fireManager.SetFireScale(value);
        }

        else if (value <= 0.2)
        {
            objectController.SetFloatSpeed(600f);
            objectController.EnableGravity(false);
            fireManager.SetFireScale(value);
        }
        else if (value <= 0.3)
        {
            objectController.SetFloatSpeed(800f);
            objectController.EnableGravity(false);
            fireManager.SetFireScale(value);
        }
        else
        {
            objectController.SetFloatSpeed(2500f);
            objectController.EnableGravity(false);
            fireManager.SetFireScale(value);
        }
    }
}
