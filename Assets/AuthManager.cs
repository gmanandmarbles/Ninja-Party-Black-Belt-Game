using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class AuthManager : MonoBehaviour
{
    int memberID = 0;
    int leaderboardID = 14540;
    private string email;
    private string password;
    private bool rememberMe;


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
                            

                            // Fetch leaderboard after starting the session
                            
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
        });
    }

    public void updateLeaderboard(int score)
    {   
    LootLockerSDKManager.SubmitScore(memberID.ToString(), score, leaderboardID, (response) =>
    {
        if (response.statusCode == 200)
        {
            Debug.Log("Successful");
        }
        else
        {
            Debug.Log("failed: " + response.Error);
        }
    });
    }

    
}

