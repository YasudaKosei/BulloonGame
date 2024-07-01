using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressedSE : MonoBehaviour
{
    public GameObject prefab; // Resourcesフォルダ内のパス

    public void Pressed()
    {
        GameObject go = Instantiate(prefab);
        Destroy(go, 1.0f);
    }
}
