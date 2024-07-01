using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLodeManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private Slider slider;

    public void LoadNextScene()
    {
        loadingUI.SetActive(true);
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("GameScene");
        while (!async.isDone)
        {
            slider.value = async.progress;
            yield return null;
        }
    }
}
