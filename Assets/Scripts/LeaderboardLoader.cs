using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;


public class LeaderboardLoader : MonoBehaviour
{
       public     string leaderboardKey = "wins";
public string memberID = "4787370";
public int count = 10;
    // Start is called before the first frame update
    void Start()
    {
       getLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getLeaderboard()
    {


LootLockerSDKManager.GetMemberRank(leaderboardKey, memberID, (response) =>
{
    if (response.statusCode == 200)
    {
        int rank = response.rank;
        
        int after = rank < 6 ? 0 : rank - 5;

        LootLockerSDKManager.GetScoreList(leaderboardKey, count, after, (response) =>
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
    else
    {
        Debug.Log("failed: " + response.Error);
    }
});
    }
}
