using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLodeManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private Slider slider;

    public void LoadNextScene(string scene)
    {
        loadingUI.SetActive(true);
        StartCoroutine(LoadScene(scene));
    }
    IEnumerator LoadScene(string scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            slider.value = async.progress;
            yield return null;
        }
    }
}
