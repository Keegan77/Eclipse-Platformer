using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneSwitch : MonoBehaviour
{
    AsyncLoader asyncLoader;
    private void Start()
    {
        asyncLoader = FindObjectOfType<AsyncLoader>();
    }
    public void OnClick()
    {
        if (asyncLoader != null)
        {
            asyncLoader.LoadLevel(1);
        }
        SceneManager.LoadScene(1);
    }
}
