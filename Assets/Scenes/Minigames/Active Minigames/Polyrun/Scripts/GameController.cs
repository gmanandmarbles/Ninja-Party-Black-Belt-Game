using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text scoreText;
    public GameObject buttonManager;
    int score = 0;

    void Start() {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        scoreText.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        Debug.Log("Dead");
        Invoke("backToBoard", 2f);
        Time.timeScale = 1;
        if(score >= 1)
        {
            Debug.Log("Less than one");
            buttonManager.GetComponent<ButtonManager>().updateScore(-10);
            
        }else{
            Debug.Log("Above one");
            buttonManager.GetComponent<ButtonManager>().updateScore(10);
        }
    }
    public void backToBoard()
    {
        Debug.Log("Before Scene Loads");
        SceneManager.LoadScene(1);
        Debug.Log("Scene should have loaded");
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
