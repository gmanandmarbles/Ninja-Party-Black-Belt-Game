using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    public void OpenScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
