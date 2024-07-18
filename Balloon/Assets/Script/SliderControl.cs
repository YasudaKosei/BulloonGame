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

    private bool onPush = false;

    public GameObject keikolkuSE;

    private GameObject go;

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

        if (BalloonManager.balloonFireLevel == 2 && !onPush)
        {
            onPush = true;
            // FillAmount�̒l�����X�Ɍ��炷
            currentFillAmount -= decreaseAmountOnPress;
        }

        if (BalloonManager.balloonFireLevel == 2 && onPush)
        {
            // FillAmount�̒l�����X�Ɍ��炷
            currentFillAmount -= decreaseSpeed * Time.deltaTime;
        }
        else if (BalloonManager.balloonFireLevel == 1)
        {
            // FillAmount�̒l�����X�Ɍ��炷
            currentFillAmount -= (decreaseSpeed / 2) * Time.deltaTime;
            onPush = false;
        }
        else
        {
            // FillAmount�̒l�����X�ɉ񕜂���
            currentFillAmount += increaseSpeed * Time.deltaTime;
            onPush = false;
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

        // ���݂�FillAmount��ۑ�
        previousFillAmount = currentFillAmount;
    }
}
