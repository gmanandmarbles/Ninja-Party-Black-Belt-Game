using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public int[] rollNumbers;  // Array of numbers to choose from
    public Text numberText;
    public GameObject movementManager;
    public GameObject diceUI;
    public bool diceOpen;
    public GameObject codey;
    public bool minigameLoading;
    public int scoremoves;

    // Array of minigame scene numbers
    public int[] minigameSceneNumbers = { 1, 2, 3, 4, 5, 6, 7 };

    // Start is called before the first frame update
    void Start()
    {
        diceUI.SetActive(false);
        minigameLoading = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollRandomNumber()
    {
        if (!minigameLoading)
        {
            int randomIndex = Random.Range(0, rollNumbers.Length);
            int rollnum = rollNumbers[randomIndex];
            numberText.text = rollnum.ToString();
            movementManager.GetComponent<MovementManager>().UpdateTurn(rollnum);
            codey.GetComponent<dice>().diceNumber(rollnum);
        }
        if (minigameLoading)
        {
            diceUI.SetActive(false);
        }
    }

    public void DiceUIToggle()
    {
        if (diceOpen == true)
        {
            diceUI.SetActive(false);
            diceOpen = false;
            //codey.GetComponent<dice>().DiceHide();
        }
        else
        {
            diceUI.SetActive(true);
            diceOpen = true;
            codey.GetComponent<dice>().diceSpawn();
        }
    }

    public void LoadMinigame()
    {
        if (minigameLoading)
        {
            int randomIndex = Random.Range(0, minigameSceneNumbers.Length);
            int sceneNumber = minigameSceneNumbers[randomIndex];
            if(sceneNumber == 11)
            {
                updateScore(10);
                Debug.Log("Hello");
            } else if (sceneNumber == 15) {
                updateScore(50);
                Debug.Log("Hello");
            } else if (sceneNumber == 16) {
                updateScore(100);
                Debug.Log("Hello");
            } else {
                updateScore(5);
                Debug.Log("Hello");
            }
            SceneManager.LoadScene(sceneNumber);
            minigameLoading = false;
        }
    }
    public void updateScore(int numbertoadd)
    {
        scoremoves = movementManager.GetComponent<MovementManager>().spacesMoved;
        movementManager.GetComponent<MovementManager>().spacesMoved = scoremoves + numbertoadd;
        

    }
}
