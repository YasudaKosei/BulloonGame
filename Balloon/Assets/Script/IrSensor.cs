using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class IrSensor : MonoBehaviour
{
    public Text textComponent; // UI�̃e�L�X�g�R���|�[�l���g
    private StringBuilder stringBuilder = new StringBuilder(1024); // ������g�ݗ��ėp��StringBuilder
    private static nn.hid.NpadId[] npadIds = { nn.hid.NpadId.No1, nn.hid.NpadId.Handheld }; // �Ή�����R���g���[���[ID�z��
    private nn.hid.NpadStyle npadStyle; // �R���g���[���[�̃X�^�C��
    private nn.Result result; // �v���Z�b�T����̌���
    private nn.irsensor.IrCameraHandle[] irCameraHandles = new nn.irsensor.IrCameraHandle[npadIds.Length]; // IR�J�����n���h���̔z��
    private nn.irsensor.IrCameraStatus irCameraStatus = new nn.irsensor.IrCameraStatus(); // IR�J�����̃X�e�[�^�X
    private nn.irsensor.MomentProcessorState momentProcessorState = new nn.irsensor.MomentProcessorState(); // ���[�����g�v���Z�b�T�̏��
    private nn.irsensor.MomentProcessorConfig momentProcessorConfig = new nn.irsensor.MomentProcessorConfig(); // ���[�����g�v���Z�b�T�̐ݒ�

    [SerializeField]
    private SpeedChanger speedChanger;

    private void Start()
    {
        // �R���g���[���[�̏����ݒ���s��
        nn.hid.Npad.Initialize();
        nn.hid.Npad.SetSupportedStyleSet(nn.hid.NpadStyle.JoyDual | nn.hid.NpadStyle.Handheld);
        nn.hid.Npad.SetSupportedIdType(npadIds);

        nn.irsensor.MomentProcessor.GetDefaultConfig(ref momentProcessorConfig);

        for (int i = 0; i < npadIds.Length; i++)
        {
            irCameraHandles[i] = nn.irsensor.IrCamera.GetHandle(npadIds[i]);
            nn.irsensor.IrCamera.Initialize(irCameraHandles[i]);
            nn.irsensor.MomentProcessor.Run(irCameraHandles[i], momentProcessorConfig);
        }
    }

    private void Update()
    {
        stringBuilder.Length = 0;
        stringBuilder.Append("IR sensor status: ");

        for (int i = 0; i < npadIds.Length; i++)
        {
            irCameraStatus = nn.irsensor.IrCamera.GetStatus(irCameraHandles[i]);

            if (irCameraStatus == nn.irsensor.IrCameraStatus.Available)
            {
                npadStyle = nn.hid.Npad.GetStyleSet(npadIds[i]);
                stringBuilder.AppendFormat("{0}\t{1}\n", irCameraStatus, npadStyle);
                EvaluateOverallCentroidState(irCameraHandles[i]);
                break;
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < npadIds.Length; i++)
        {
            nn.irsensor.ImageProcessor.Stop(irCameraHandles[i]);
            nn.irsensor.IrCamera.Finalize(irCameraHandles[i]);
        }
    }

    private void EvaluateOverallCentroidState(nn.irsensor.IrCameraHandle handle)
    {
        result = nn.irsensor.MomentProcessor.GetState(ref momentProcessorState, handle);

        if (!result.IsSuccess())
        {
            stringBuilder.Append("Error: " + result.ToString() + "\n");
            return;
        }

        int totalBlocks = momentProcessorState.blocks.Length;
        float totalUsedArea = 0;

        for (int i = 0; i < totalBlocks; i++)
        {
            // �����ŁA�e�u���b�N�̕��ϋ��x�⑼�̎w�W���g�p���āA�g�p����Ă���ʐς̊������v�Z
            // �Ⴆ�΁A���x������臒l�ȏ�ł���΂��̃u���b�N�́u�g�p���v�Ƃ݂Ȃ�
            if (momentProcessorState.blocks[i].averageIntensity > 40) // 臒l�͓K�X����
            {
                totalUsedArea += 1; // ���̃u���b�N�͎g�p����Ă���ƃJ�E���g
            }
        }

        float usagePercentage = (totalUsedArea / totalBlocks) * 100; // �g�p�����p�[�Z���e�[�W�Ōv�Z

        float usageVal = totalUsedArea / totalBlocks; // 0����1�͈̔͂ɒ���

        speedChanger.ChangeFloatSpeed(float.Parse(usageVal.ToString("F2")));

        // UI�Ɏg�p����\��
        stringBuilder.AppendLine($"Sensor used area: {usagePercentage:F2}%");
        textComponent.text = stringBuilder.ToString();
    }

}
