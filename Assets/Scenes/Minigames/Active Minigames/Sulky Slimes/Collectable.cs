using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    [Header("Scene to load")]
    public int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Invoke("loadNextScene", 2f);
        }
    }
    public void loadNextScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
