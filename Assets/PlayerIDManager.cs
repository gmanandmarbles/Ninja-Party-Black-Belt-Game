using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;
using LootLocker;

public class PlayerIDManager : MonoBehaviour
{
    int memberID = 0;
    int leaderboardID = 14540;
    public Text idText;
    private string email;
    private string password;
    private bool rememberMe;
    public GameObject leaderboardManager;

    void Start()
    {
        email = PlayerPrefs.GetString("email");
        password = PlayerPrefs.GetString("password");
        rememberMe = true;

        LootLockerSDKManager.WhiteLabelLoginAndStartSession(email, password, rememberMe, (response) =>
        {
            if (!response.success)
            {
                if (!response.LoginResponse.success)
                {
                    Debug.Log("Error while logging in");
                    Debug.Log("Session is not valid, starting guest session...");

                    LootLockerSDKManager.StartGuestSession((guestSessionResponse) =>
                    {
                        if (guestSessionResponse.success)
                        {
                            memberID = guestSessionResponse.player_id;
                            Debug.Log("Player ID: " + memberID);

                            Debug.Log("Successfully started LootLocker session");
                            idText.text = "You are: " + memberID;

                            // Fetch leaderboard after starting the session
                            StartCoroutine(leaderboardManager.GetComponent<LeaderBoardManager>().FetchTopHighscoresRoutine());

                            // Retrieve player name
                            LootLockerSDKManager.GetPlayerName((nameResponse) =>
                            {
                                if (nameResponse.success)
                                {
                                    Debug.Log("Successfully retrieved player name: " + nameResponse.name);
                                    idText.text += ", Player Name: " + nameResponse.name;
                                }
                                else
                                {
                                    Debug.Log("Error getting player name. Falling back to player ID.");
                                    idText.text += ", Player Name: " + memberID.ToString();
                                }
                            });
                        }
                        else
                        {
                            Debug.Log("Error starting LootLocker session: " + guestSessionResponse.Error);
                        }
                    });
                }
                else if (!response.SessionResponse.success)
                {
                    Debug.Log("Error while starting session");
                    Debug.Log("Session is not valid, starting guest session...");
                }
                return;
            }

            // Handle Returning Player
            memberID = response.SessionResponse.player_id;
            Debug.Log("Player ID: " + memberID);

            Debug.Log("Successfully started LootLocker session");
            idText.text = "You are: " + memberID;

            // Fetch leaderboard after starting the session
            StartCoroutine(leaderboardManager.GetComponent<LeaderBoardManager>().FetchTopHighscoresRoutine());

            // Retrieve player name
            LootLockerSDKManager.GetPlayerName((nameResponse) =>
            {
                if (nameResponse.success)
                {
                    Debug.Log("Successfully retrieved player name: " + nameResponse.name);
                    idText.text += ", Player Name: " + nameResponse.name;
                }
                else
                {
                    Debug.Log("Error getting player name. Falling back to player ID.");
                    idText.text += ", Player Name: " + memberID.ToString();
                }
            });
        });
    }
}
