using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private float floatSpeed = 0f; // 浮く速度
    public float rotateSpeed = 0f; // 回転速度
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false; // オブジェクトに重力を無効にする

        EnableGravity(true);
    }

    void Update()
    {
        // オブジェクトを浮かせる
        FloatObject();

        // 左右に回転(pc)
        RotateObjectPC();
    }

    void FloatObject()
    {
        // オブジェクトに上向きの力を加える
        Vector3 floatForce = transform.up * (floatSpeed * Time.deltaTime);
        rigidBody.AddForce(floatForce, ForceMode.Force);
    }

    void RotateObjectPC()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (transform.rotation.z < 0.56)
            {
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotateSpeed));
            }
        }
        else if (Input.GetKey(KeyCode.C))
        {
            Debug.Log(transform.rotation.z);
            if(transform.rotation.z > -0.56)
            {
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * -rotateSpeed));
            }
        }

    }

    public void RotateObjectSwitch(float val)
    {
        //if(val > 280)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, 280 * -1);
        //}
        //else if (val < 90)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, 90 * -1);
        //}
        //else
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, (val) * -1);
        //}

        transform.rotation = Quaternion.Euler(0, 0, (val) * -1);
    }

    public void SetFloatSpeed(float value)
    {
        floatSpeed = value * 0.2f;
    }

    public void EnableGravity(bool enable)
    {
        rigidBody.useGravity = enable;
    }
}
