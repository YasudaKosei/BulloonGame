using UnityEngine;
using UnityEngine.UI;

public class SpeedChanger : MonoBehaviour
{
    //Balloonのスピード管理

    public ObjectController objectControllerScript;
    public FireScale fireScale;

    public void ChangeFloatSpeed(float value)
    {
        value = 1 - value;

        if (value <= 0.0)
        {
            objectControllerScript.SetFloatSpeed(0);
            objectControllerScript.EnableGravity(true);
            fireScale.SetFireScale(value);
        }
        else if (value <= 0.1)
        {
            objectControllerScript.SetFloatSpeed(400);
            objectControllerScript.EnableGravity(false);
            fireScale.SetFireScale(value);
        }

        else if (value <= 0.2)
        {
            objectControllerScript.SetFloatSpeed(600f);
            objectControllerScript.EnableGravity(false);
            fireScale.SetFireScale(value);
        }
        else if (value <= 0.3)
        {
            objectControllerScript.SetFloatSpeed(800f);
            objectControllerScript.EnableGravity(false);
            fireScale.SetFireScale(value);
        }
        else
        {
            objectControllerScript.SetFloatSpeed(2500f);
            objectControllerScript.EnableGravity(false);
            fireScale.SetFireScale(value);
        }
    }
}
