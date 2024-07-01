using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelectAfterDelay : MonoBehaviour
{
    private Button buttonToSelect; // �I���������{�^��
    public float delay = 1.0f;     // �ҋ@����b��

    void OnEnable()
    {
        buttonToSelect = GetComponent<Button>();
        StartCoroutine(SelectButtonAfterDelay());
    }

    private IEnumerator SelectButtonAfterDelay()
    {
        // �w�肵���b���ҋ@
        yield return new WaitForSeconds(delay);

        // �{�^����I��
        EventSystem.current.SetSelectedGameObject(buttonToSelect.gameObject);
    }
}
