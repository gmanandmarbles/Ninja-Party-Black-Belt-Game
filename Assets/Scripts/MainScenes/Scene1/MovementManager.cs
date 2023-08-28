using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementManager : MonoBehaviour
{
    public int tileNumber;
    public float speed = 100000000.0f;
    public int currentTileNumber = 0;
    private Vector3 target;
    public GameObject player;
    public GameObject buttonManager;
    public Text scoreText;
    public AuthManager authManager; // Reference to the AuthManager component
    public GameObject leaderboardUpdate;

    private bool rolledDice = false;
    private long timeSinceMoved = System.DateTime.Now.Ticks;

    public int spacesMoved = 0; 

    // Start is called before the first frame update
    void Start()
    {
        tileNumber = PlayerPrefs.GetInt("TileNumber", 1);
        currentTileNumber = tileNumber;

        spacesMoved = PlayerPrefs.GetInt("SpacesMoved", 0); // Load the spaces moved from PlayerPrefs
        scoreText.text = "Score: " + spacesMoved; // Update the score text directly with the spacesMoved variable
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTileNumber < tileNumber && System.DateTime.Now.Ticks - timeSinceMoved > 10000000L)
        {
            timeSinceMoved = System.DateTime.Now.Ticks;
            currentTileNumber += 1;
            scoreText.text = "Score: " + spacesMoved;

            PlayerPrefs.SetInt("SpacesMoved", spacesMoved); // Save spaces moved to PlayerPrefs
            PlayerPrefs.Save();
        }

        if (rolledDice && currentTileNumber == tileNumber && buttonManager.GetComponent<ButtonManager>().minigameLoading == false)
        {
            buttonManager.GetComponent<ButtonManager>().minigameLoading = true;

            // Detect the tag of the final spot
            GameObject finalSpot = GameObject.Find("/GameTiles/" + currentTileNumber);
            if (finalSpot != null)
            {
                string tag = finalSpot.tag;

                // Increment value based on tag
                switch (tag)
                {
                    case "WhiteSpace":
                        spacesMoved += 2;
                        break;
                    case "BlueSpace":
                        spacesMoved += 1;
                        break;
                    case "RedSpace":
                        spacesMoved += 10;
                        break;
                    // Add more cases for other tag values if needed

                    default:
                        break;
                }

                // Update score text and save spaces moved
                scoreText.text = "Score: " + spacesMoved;
                PlayerPrefs.SetInt("SpacesMoved", spacesMoved);
                PlayerPrefs.Save();

                // Update the leaderboard via AuthManager
                authManager.updateLeaderboard(PlayerPrefs.GetInt("SpacesMoved"));
            }

            buttonManager.GetComponent<ButtonManager>().Invoke("LoadMinigame", 2f);
        }

        target = GameObject.Find("/GameTiles/" + currentTileNumber).transform.position;
        player.transform.position = target;

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            tileNumber = 1;
            currentTileNumber = 1;
            spacesMoved = 0; // Reset spaces moved
            PlayerPrefs.SetInt("SpacesMoved", spacesMoved); // Save reset spaces moved to PlayerPrefs
            PlayerPrefs.Save();
        }
    }

    public void UpdateTurn(int rolledNum)
    {
        tileNumber += rolledNum;
        PlayerPrefs.SetInt("Space", tileNumber);
        rolledDice = true;

        buttonManager.GetComponent<ButtonManager>().DiceUIToggle();

        PlayerPrefs.SetInt("TileNumber", tileNumber);
        PlayerPrefs.Save();
    }
}
