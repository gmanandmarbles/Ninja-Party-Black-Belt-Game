using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class updateLeaderboard : MonoBehaviour
{
    private int memberID = 0;
    private int leaderboardID = 14540;
    // Start is called before the first frame update
    void Start()
    {
        memberID = PlayerPrefs.GetInt("member");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void leaderboardUpdate(int score)
    {
        LootLockerSDKManager.SubmitScore(memberID.ToString(), score, leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
            }
            else
            {
                Debug.Log("Failed: " + response.Error);
            }
        });
    }
}
