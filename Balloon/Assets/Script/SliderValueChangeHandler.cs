using UnityEngine;
using UnityEngine.UI;

public class SliderValueChangeHandler : MonoBehaviour
{
   //PCデバッグ用のスライダー管理

    public Slider slider;

    [SerializeField]
    private SpeedChanger speedChanger;

    void Start()
    {
        // スライダーのOnValueChangedイベントにリスナーを登録
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    // スライダーの値が変わった時に呼ばれる関数
    void OnSliderValueChanged(float value)
    {
        speedChanger.ChangeFloatSpeed(value);
    }

    void OnDestroy()
    {
        // スライダーのOnValueChangedイベントからリスナーを解除
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}
