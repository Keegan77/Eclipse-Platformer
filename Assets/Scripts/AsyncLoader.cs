using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    [SerializeField] GameObject spinningIcon;
    [SerializeField] float rotationSpeed;
    
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
            spinningIcon.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
            yield return null;
        }
    }
}
