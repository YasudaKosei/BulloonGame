using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SliderControl : MonoBehaviour
{
    public Image image; // �C���[�W�̎Q�Ƃ�ݒ肷��
    public float decreaseSpeed = 0.1f; // FillAmount�����鑬�x��ݒ肷��
    public float increaseSpeed = 0.05f; // FillAmount���񕜂��鑬�x��ݒ肷��

    public float decreaseAmountOnPress = 0.05f;

    private float previousFillAmount;

    public ObjectController objectController;

    private Color defaultColor;
    private Color yellowColor = Color.yellow;
    private Color redColor = Color.red;

    void Start()
    {
        // ������FillAmount��ۑ�
        previousFillAmount = image.fillAmount;
        defaultColor = image.color; // �����̐F��ۑ�
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
            currentFillAmount -= decreaseAmountOnPress; // �w�肵���l�����炷
        }

        if (Gamepad.current != null && Gamepad.current.buttonNorth.isPressed)
        {
            // FillAmount�̒l�����X�Ɍ��炷
            currentFillAmount -= decreaseSpeed * Time.deltaTime;
        }
        else if (Gamepad.current != null && Gamepad.current.buttonEast.isPressed)
        {
            // FillAmount�̒l�����X�Ɍ��炷
            currentFillAmount -= (increaseSpeed / 2) * Time.deltaTime;
        }
        else
        {
            // FillAmount�̒l�����X�ɉ񕜂���
            currentFillAmount += increaseSpeed * Time.deltaTime;
        }

        // FillAmount�̒l��0����1�͈͓̔��ɐ�������
        currentFillAmount = Mathf.Clamp(currentFillAmount, 0f, 1f);

        // FillAmount���ς���Ă���ꍇ�ɂ̂݃C���[�W��\������
        if (currentFillAmount != previousFillAmount)
        {
            image.enabled = true;
            image.fillAmount = currentFillAmount;

            // FillAmount�Ɋ�Â��ĐF��ύX
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

        // ���݂�FillAmount��ۑ�
        previousFillAmount = currentFillAmount;
    }
}
