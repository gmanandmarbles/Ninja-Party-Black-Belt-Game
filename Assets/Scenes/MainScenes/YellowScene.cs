using UnityEngine;
using UnityEngine.SceneManagement;

public class YellowScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("YellowSpace"))
        {

             PlayerPrefs.DeleteKey("TileNumber");
            SceneManager.LoadScene(8);
        } 
    }
}
