using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    public GameObject loadingScreen;

    public void LoadLevel(int sceneId)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(sceneId));
    }

    IEnumerator LoadLevelAsync(int sceneId)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneId);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }
}
