using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelectSE : MonoBehaviour, ISelectHandler
{
    private Button button;
    public GameObject prefab; // Resourcesフォルダ内のパス

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameObject go = Instantiate(prefab);
        Destroy(go, 1.0f);
    }

}
