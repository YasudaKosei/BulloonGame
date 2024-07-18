using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    public static bool isFalling = false;

    public static bool wait = true;

    public static int controllerNum = 0;

    public static int hp = 3;

    public static float floatSpeed = 0f; // 浮く速度

    public static float rotateSpeed = 200f; // 回転速度

    public static int balloonFireLevel = 0;

    private void Start()
    {
        isFalling = false;

        wait = true;

        controllerNum = 0;

        hp = 3;

        floatSpeed = 0f; // 浮く速度

        rotateSpeed = 200f; // 回転速度

        balloonFireLevel = 0;
    }
}
