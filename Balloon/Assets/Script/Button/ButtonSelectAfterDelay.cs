using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelectAfterDelay : MonoBehaviour
{
    private Button buttonToSelect; // 選択したいボタン
    public float delay = 1.0f;     // 待機する秒数

    void OnEnable()
    {
        buttonToSelect = GetComponent<Button>();
        StartCoroutine(SelectButtonAfterDelay());
    }

    private IEnumerator SelectButtonAfterDelay()
    {
        // 指定した秒数待機
        yield return new WaitForSeconds(delay);

        // ボタンを選択
        EventSystem.current.SetSelectedGameObject(buttonToSelect.gameObject);
    }
}
