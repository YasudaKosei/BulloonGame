using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressedSE : MonoBehaviour
{
    public GameObject prefab; // Resources�t�H���_���̃p�X

    public void Pressed()
    {
        GameObject go = Instantiate(prefab);
        Destroy(go, 1.0f);
    }
}
