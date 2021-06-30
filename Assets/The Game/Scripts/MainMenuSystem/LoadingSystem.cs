using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSystem : MonoBehaviour
{
    [Header("Required Compoenent")]
    [SerializeField] private GameObject camera;
    [SerializeField] private string loadNextLevel;
    [SerializeField] private Image loadingBar;
    
    private void Start()
    {
        camera.SetActive(true);

        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {

        AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(loadNextLevel, LoadSceneMode.Additive);
        loadSceneOperation.allowSceneActivation = false;

        while (!loadSceneOperation.isDone)
        {
            loadingBar.fillAmount = loadSceneOperation.progress * 0.1f;

            if (loadSceneOperation.progress >= .0f)
            {
                loadingBar.fillAmount = 1;
                yield return new WaitForSeconds(1f);
                loadSceneOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        SceneManager.UnloadSceneAsync("Loading Screen");
        SceneManager.UnloadSceneAsync("Loading Screen 1");
    }
}
