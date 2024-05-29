using UnityEngine;
using UnityEngine.UI;

public class SliderValueChangeHandler : MonoBehaviour
{
   //PC�f�o�b�O�p�̃X���C�_�[�Ǘ�

    public Slider slider;

    [SerializeField]
    private SpeedChanger speedChanger;

    void Start()
    {
        // �X���C�_�[��OnValueChanged�C�x���g�Ƀ��X�i�[��o�^
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    // �X���C�_�[�̒l���ς�������ɌĂ΂��֐�
    void OnSliderValueChanged(float value)
    {
        speedChanger.ChangeFloatSpeed(value);
    }

    void OnDestroy()
    {
        // �X���C�_�[��OnValueChanged�C�x���g���烊�X�i�[������
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}
