using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class IrSensor : MonoBehaviour
{
    public Text textComponent; // UIのテキストコンポーネント
    private StringBuilder stringBuilder = new StringBuilder(1024); // 文字列組み立て用のStringBuilder
    private static nn.hid.NpadId[] npadIds = { nn.hid.NpadId.No1, nn.hid.NpadId.Handheld }; // 対応するコントローラーID配列
    private nn.hid.NpadStyle npadStyle; // コントローラーのスタイル
    private nn.Result result; // プロセッサからの結果
    private nn.irsensor.IrCameraHandle[] irCameraHandles = new nn.irsensor.IrCameraHandle[npadIds.Length]; // IRカメラハンドルの配列
    private nn.irsensor.IrCameraStatus irCameraStatus = new nn.irsensor.IrCameraStatus(); // IRカメラのステータス
    private nn.irsensor.MomentProcessorState momentProcessorState = new nn.irsensor.MomentProcessorState(); // モーメントプロセッサの状態
    private nn.irsensor.MomentProcessorConfig momentProcessorConfig = new nn.irsensor.MomentProcessorConfig(); // モーメントプロセッサの設定

    [SerializeField]
    private SpeedChanger speedChanger;

    private void Start()
    {
        // コントローラーの初期設定を行う
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
            // ここで、各ブロックの平均強度や他の指標を使用して、使用されている面積の割合を計算
            // 例えば、強度がある閾値以上であればそのブロックは「使用中」とみなす
            if (momentProcessorState.blocks[i].averageIntensity > 40) // 閾値は適宜調整
            {
                totalUsedArea += 1; // このブロックは使用されているとカウント
            }
        }

        float usagePercentage = (totalUsedArea / totalBlocks) * 100; // 使用率をパーセンテージで計算

        float usageVal = totalUsedArea / totalBlocks; // 0から1の範囲に調整

        speedChanger.ChangeFloatSpeed(float.Parse(usageVal.ToString("F2")));

        // UIに使用率を表示
        stringBuilder.AppendLine($"Sensor used area: {usagePercentage:F2}%");
        textComponent.text = stringBuilder.ToString();
    }

}
