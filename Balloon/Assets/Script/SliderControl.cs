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

        if (Gamepad.current != null && Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            currentFillAmount -= decreaseAmountOnPress; // 指定した値を減らす
        }

        if (Gamepad.current != null && Gamepad.current.buttonNorth.isPressed)
        {
            // FillAmountの値を徐々に減らす
            currentFillAmount -= decreaseSpeed * Time.deltaTime;
        }
        else if (Gamepad.current != null && Gamepad.current.buttonEast.isPressed)
        {
            // FillAmountの値を徐々に減らす
            currentFillAmount -= (increaseSpeed / 2) * Time.deltaTime;
        }
        else
        {
            // FillAmountの値を徐々に回復する
            currentFillAmount += increaseSpeed * Time.deltaTime;
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
                image.color = redColor;
            }
            else if (currentFillAmount <= 0.5f)
            {
                image.color = yellowColor;
            }
            else
            {
                image.color = defaultColor;
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
