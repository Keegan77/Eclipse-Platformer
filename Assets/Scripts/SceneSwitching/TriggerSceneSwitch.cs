using UnityEngine.SceneManagement;
using UnityEngine;

public class TriggerSceneSwitch : MonoBehaviour
{
    [SerializeField] int sceneId;
    AsyncLoader asyncLoader;

    private void Start()
    {
        asyncLoader = FindObjectOfType<AsyncLoader>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (asyncLoader != null)
        {
            asyncLoader.LoadLevel(sceneId);
        } else
        {
            SceneManager.LoadScene(sceneId);
        }
        
    }
}
