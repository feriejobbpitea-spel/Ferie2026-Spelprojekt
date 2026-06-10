using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;


public class SceneLoader : PersistentSingleton<SceneLoader>
{
    private const string loadingScreenSceneName = "LoadingScreen";

    private AsyncOperation asyncLoad;
    private Coroutine loadCoroutine;

    public static Action<SceneLoaderStates> OnSceneLoaderStateChanged;
    public static Action<int> OnSceneProgressChanged;

    private void OnDisable()
    {
        if (loadCoroutine != null)
            StopCoroutine(loadCoroutine);
    }

    public static void LoadScene(string sceneName)
    {
        if(Instance == null)
        {
            Debug.LogError("SceneLoader instance is not initialized.");
            return;
        }

        Instance.Internal_LoadScene(sceneName);
    }

    protected void Internal_LoadScene(string sceneName) 
    {
        if(loadCoroutine != null)
            StopCoroutine(loadCoroutine);

        loadCoroutine = StartCoroutine(LoadAsyncScene(sceneName));
    }

    protected IEnumerator LoadAsyncScene(string sceneToLoad)
    {
        // Open the loading screen scene
        SceneManager.LoadScene(loadingScreenSceneName, LoadSceneMode.Single);

        OnSceneLoaderStateChanged?.Invoke(SceneLoaderStates.Loading);
        OnSceneProgressChanged?.Invoke(0);


        yield return new WaitForSecondsRealtime(1F);

        asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        //wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            OnSceneProgressChanged?.Invoke((int)(asyncLoad.progress * 100));

            // scene has loaded as much as possible,
            // the last 10% can't be multi-threaded
            if (asyncLoad.progress >= 0.9f)
            {
                OnSceneProgressChanged?.Invoke(100);
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }


        // Finished loading the scene
        OnSceneLoaderStateChanged?.Invoke(SceneLoaderStates.FinishedLoading);
        OnSceneProgressChanged?.Invoke(100);
    }
}
