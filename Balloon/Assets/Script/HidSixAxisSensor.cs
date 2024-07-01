using UnityEngine;
using UnityEngine.UI;
using nn.hid;

public class HidSixAxisSensor : MonoBehaviour // MonoBehaviourを継承したクラス
{
    public ObjectController objectController;

    // UIテキストを表示するためのコンポーネント
    public Text textComponent;
    // 文字列を動的に組み立てるためのStringBuilderインスタンス
    private System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

    // コントローラーのID、スタイル、状態を管理する変数
    private NpadId npadId = NpadId.Invalid; // コントローラーのID（初期値：無効）
    private NpadStyle npadStyle = NpadStyle.Invalid; // コントローラースタイル（初期値：無効）
    private NpadState npadState = new NpadState(); // コントローラーの状態

    // 6軸センサーのハンドルを格納する配列（最大2つ）
    private SixAxisSensorHandle[] handle = new SixAxisSensorHandle[2];
    // 6軸センサーの状態を格納する変数
    private SixAxisSensorState state = new SixAxisSensorState();
    // 実際に使用するセンサーハンドルの数
    private int handleCount = 0;

    // Npadから得られるクォータニオンを格納する変数
    private nn.util.Float4 npadQuaternion = new nn.util.Float4();
    // Unityで使用するクォータニオン
    private Quaternion quaternion = new Quaternion();

    void Start() // スクリプト開始時に呼ばれるメソッド
    {
        // Npadの初期化
        Npad.Initialize();
        // 対応するコントローラースタイルの設定
        Npad.SetSupportedStyleSet(NpadStyle.Handheld | NpadStyle.JoyDual | NpadStyle.FullKey);
        // 対応するコントローラーIDの設定
        NpadId[] npadIds = { NpadId.Handheld, NpadId.No1 };
        Npad.SetSupportedIdType(npadIds);
    }

    void Update()
    {
        if (BalloonManager.controllerNum != 1) return;

        stringBuilder.Length = 0;

        NpadId npadId = NpadId.Handheld;
        NpadStyle npadStyle = NpadStyle.None;

        npadStyle = Npad.GetStyleSet(npadId);

        if (npadStyle != NpadStyle.Handheld)
        {
            npadId = NpadId.No1;
            npadStyle = Npad.GetStyleSet(npadId);
        }

        if (UpdatePadState())
        {
            for (int i = 0; i < handleCount; i++)
            {
                SixAxisSensor.GetState(ref state, handle[i]);

                state.GetQuaternion(ref npadQuaternion);
                quaternion.Set(npadQuaternion.x, npadQuaternion.z, npadQuaternion.y, -npadQuaternion.w);

                // オブジェクトの回転を360度形式で取得
                Vector3 rotation360 = quaternion.eulerAngles;
                rotation360 = new Vector3(
                    rotation360.x >= 0 ? rotation360.x : 360 + rotation360.x,
                    rotation360.y >= 0 ? rotation360.y : 360 + rotation360.y,
                    rotation360.z >= 0 ? rotation360.z : 360 + rotation360.z
                );

                stringBuilder.AppendFormat(
                    "{0}[{1}]:\rotation.z {2:F1}\n",
                    npadStyle.ToString(), i,rotation360.z
                );

                objectController.RotateObjectSwitch(rotation360.z);
            }
        }

        textComponent.text = stringBuilder.ToString();
    }

    private bool UpdatePadState() // パッドの状態を更新するメソッド
    {
        // ハンドヘルドスタイルの状態を取得
        NpadStyle handheldStyle = Npad.GetStyleSet(NpadId.Handheld);
        NpadState handheldState = npadState;
        if (handheldStyle != NpadStyle.None)
        {
            Npad.GetState(ref handheldState, NpadId.Handheld, handheldStyle);
            if (handheldState.buttons != NpadButton.None)
            {
                if ((npadId != NpadId.Handheld) || (npadStyle != handheldStyle))
                {
                    this.GetSixAxisSensor(NpadId.Handheld, handheldStyle);
                }
                npadId = NpadId.Handheld;
                npadStyle = handheldStyle;
                npadState = handheldState;
                return true;
            }
        }

        // No1スタイルの状態を取得
        NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
        NpadState no1State = npadState;
        if (no1Style != NpadStyle.None)
        {
            Npad.GetState(ref no1State, NpadId.No1, no1Style);
            if ((npadId != NpadId.No1) || (npadStyle != no1Style))
            {
                this.GetSixAxisSensor(NpadId.No1, no1Style);
            }
            npadId = NpadId.No1;
            npadStyle = no1Style;
            npadState = no1State;
            return true;
        }

        return false;
    }

    private void GetSixAxisSensor(NpadId id, NpadStyle style) // 6軸センサーを取得して開始するメソッド
    {
        // 既存のセンサーを停止
        for (int i = 0; i < handleCount; i++)
        {
            SixAxisSensor.Stop(handle[i]);
        }

        // 新しいセンサーハンドルを取得
        handleCount = SixAxisSensor.GetHandles(handle, 2, id, style);

        // 新しいセンサーを開始
        for (int i = 0; i < handleCount; i++)
        {
            SixAxisSensor.Start(handle[i]);
        }
    }
}
