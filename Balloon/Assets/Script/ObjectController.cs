using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectController : MonoBehaviour
{
    public CapsuleCollider ObjectCollider;
    public Rigidbody RigidBody;

    public GameObject smake;

    public GameObject buff;
    public Transform buffPos;

    public FireManager fireManager;

    public float[] fireSizeSpeed;

    public GameObject[] hpSmoke;

    public CameraShake cameraShake;

    void Update()
    {
        if (BalloonManager.isFalling || BalloonManager.wait) return;

        // オブジェクトを浮かせる
        FloatObject();

        RotateObjectPC();
        RotateObjectSwitch();

    }

    // オブジェクトを浮かせる
    void FloatObject()
    {
        // オブジェクトに上向きの力を加える
        Vector3 floatForce = transform.up * (BalloonManager.floatSpeed * Time.deltaTime);
        RigidBody.AddForce(floatForce, ForceMode.Force);
    }

    // 左右に回転(pc)
    void RotateObjectPC()
    {
        if (BalloonManager.controllerNum != 0) return;

        if (Input.GetKey(KeyCode.Z))
        {
            if (transform.rotation.z < 0.56)
            {
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * BalloonManager.rotateSpeed));
            }
        }
        else if (Input.GetKey(KeyCode.C))
        {
            Debug.Log(transform.rotation.z);
            if(transform.rotation.z > -0.56)
            {
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * -BalloonManager.rotateSpeed));
            }
        }
    }

    void RotateObjectSwitch()
    {
        if (BalloonManager.controllerNum != 0 || Gamepad.current == null) return;

        if (Gamepad.current.leftStick.ReadValue().x < -0.1f)
        {
            if (transform.rotation.z < 0.56)
            {
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * BalloonManager.rotateSpeed));
            }
        }
        else if (Gamepad.current.leftStick.ReadValue().x > 0.1f)
        {
            Debug.Log(transform.rotation.z);
            if (transform.rotation.z > -0.56)
            {
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * -BalloonManager.rotateSpeed));
            }
        }
    }

    public void RotateObjectSwitch(float val)
    {
        transform.rotation = Quaternion.Euler(0, 0, (val) * -1);
    }

    public void SetFloatSpeed()
    {
        BalloonManager.floatSpeed = fireSizeSpeed[BalloonManager.balloonFireLevel];
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HealPoint")
        {
            if (BalloonManager.hp == 3) return;
            Debug.Log("HealPointに当たりました");
            BalloonManager.hp = 3;
            HPSmoke();
            Instantiate(buff, buffPos);
        }
    }

    public int HP(int val)
    {
        BalloonManager.hp = BalloonManager.hp - val;
        HPSmoke();
        cameraShake.Shake();
        return BalloonManager.hp;
    }

    public void HPSmoke()
    {
        if (BalloonManager.hp == 3)
        {
            DownBalloonOff();
            for(int i = 0; i < hpSmoke.Length; i++)
            {
                hpSmoke[i].SetActive(false);
            }
        }

        if (BalloonManager.hp < 3)
        {
            hpSmoke[0].SetActive(true);

            if (BalloonManager.hp < 2)
            {
                hpSmoke[1].SetActive(true);
            }
        }

        if (BalloonManager.hp == 0) DownBalloonOn();
    }

    public void DownBalloonOn()
    {
        BalloonManager.isFalling = true;
        smake.SetActive(true);
        fireManager.SetFireScale();
        gameObject.layer = LayerMask.NameToLayer("FallingBalloon");
    }

    public void DownBalloonOff()
    {
        BalloonManager.isFalling = false;
        smake.SetActive(false);
        fireManager.SetFireScale();
        Instantiate(buff, buffPos);
        gameObject.layer = LayerMask.NameToLayer("Balloon");
    }
}
