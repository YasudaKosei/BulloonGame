using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SliderControl : MonoBehaviour
{
    public Image image; // イメージの参照を設定する
    public float decreaseSpeed = 0.1f; // FillAmountが減る速度を設定する
    public float increaseSpeed = 0.05f; // FillAmountが回復する速度を設定する

    public float decreaseAmountOnPress = 0.05f;

    private float previousFillAmount;

    public ObjectController objectController;

    private Color defaultColor;
    private Color yellowColor = Color.yellow;
    private Color redColor = Color.red;

    private bool onPush = false;

    public GameObject keikolkuSE;

    private GameObject go;

    void Start()
    {
        // 初期のFillAmountを保存
        previousFillAmount = image.fillAmount;
        defaultColor = image.color; // 初期の色を保存
    }

    void Update()
    {
        if (BalloonManager.isFalling || BalloonManager.wait)
        {
            image.enabled = false;
            return;
        }

        float currentFillAmount = image.fillAmount;

        if (currentFillAmount <= 0)
        {
            currentFillAmount = 1;
            objectController.HP(3);
        }

        if (BalloonManager.balloonFireLevel == 2 && !onPush)
        {
            onPush = true;
            // FillAmountの値を徐々に減らす
            currentFillAmount -= decreaseAmountOnPress;
        }

        if (BalloonManager.balloonFireLevel == 2 && onPush)
        {
            // FillAmountの値を徐々に減らす
            currentFillAmount -= decreaseSpeed * Time.deltaTime;
        }
        else if (BalloonManager.balloonFireLevel == 1)
        {
            // FillAmountの値を徐々に減らす
            currentFillAmount -= (decreaseSpeed / 2) * Time.deltaTime;
            onPush = false;
        }
        else
        {
            // FillAmountの値を徐々に回復する
            currentFillAmount += increaseSpeed * Time.deltaTime;
            onPush = false;
        }

        // FillAmountの値を0から1の範囲内に制限する
        currentFillAmount = Mathf.Clamp(currentFillAmount, 0f, 1f);

        // FillAmountが変わっている場合にのみイメージを表示する
        if (currentFillAmount != previousFillAmount)
        {
            image.enabled = true;
            image.fillAmount = currentFillAmount;

            // FillAmountに基づいて色を変更
            if (currentFillAmount <= 0.2f)
            {
                if(go == null)
                {
                    go = Instantiate(keikolkuSE);
                    Destroy(go, 1.0f);
                }
                image.color = redColor;
            }
            else if (currentFillAmount <= 0.5f)
            {
                image.color = yellowColor;
                if (go)
                {
                    Destroy(go);
                }
            }
            else
            {
                image.color = defaultColor;
                if (go)
                {
                    Destroy(go);
                }
            }
        }
        else
        {
            image.enabled = false;
        }

        // 現在のFillAmountを保存
        previousFillAmount = currentFillAmount;
    }
}
