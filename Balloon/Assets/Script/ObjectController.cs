using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private float floatSpeed = 0f; // 浮く速度
    public float rotateSpeed = 0f; // 回転速度
    public static int hp = 3;
    public static bool ObjectMove;

    public CapsuleCollider ObjectCollider;
    public Rigidbody RigidBody;

    void Start()
    {
        ObjectMove = true;
        EnableGravity(true);
    }

    void Update()
    {
        if (!ObjectMove) return;

        // オブジェクトを浮かせる
        FloatObject();

#if UNITY_EDITOR
        // 左右に回転(pc)
        RotateObjectPC();
#endif
    }

    // オブジェクトを浮かせる
    void FloatObject()
    {
        // オブジェクトに上向きの力を加える
        Vector3 floatForce = transform.up * (floatSpeed * Time.deltaTime);
        RigidBody.AddForce(floatForce, ForceMode.Force);
    }

    // 左右に回転(pc)
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
        transform.rotation = Quaternion.Euler(0, 0, (val) * -1);
    }

    public void SetFloatSpeed(float value)
    {
        floatSpeed = value * 0.2f;
    }

    public void EnableGravity(bool enable)
    {
        RigidBody.useGravity = enable;
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HealPoint")
        {
            Debug.Log("HealPointに当たりました");
            hp = 3;
            DownBalloonOff();
        }
    }

    public int HP(int val)
    {
        hp = hp - val;
        if (hp == 0) DownBalloonOn();

        return hp;
    }

    public void DownBalloonOn()
    {
        ObjectMove = false;
        ObjectCollider.isTrigger = true;
    }

    public void DownBalloonOff()
    {
        ObjectMove = true;
        ObjectCollider.isTrigger = false;
    }
}
