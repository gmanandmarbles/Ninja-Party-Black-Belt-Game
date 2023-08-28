using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LivesManager : MonoBehaviour
{
    public int lives;
    public GameObject[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RemoveLife()
    {
        lives -= 1;
        print("You lost a life! Lives: " + lives);
        hearts[lives].SetActive(false);
        if (lives == 0)
        {
             Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

    }
}
