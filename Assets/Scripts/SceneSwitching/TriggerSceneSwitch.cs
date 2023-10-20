using UnityEngine.SceneManagement;
using UnityEngine;

public class TriggerSceneSwitch : MonoBehaviour
{
    [SerializeField] int sceneId;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneId);
    }
}
