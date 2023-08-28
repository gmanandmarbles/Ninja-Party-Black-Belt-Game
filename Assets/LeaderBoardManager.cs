using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LeaderBoardManager : MonoBehaviour
{
    int leaderboardID = 14540;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;

    // Start is called before the first frame update
    void Start()
    {

    
}
public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        Debug.Log("Fetching top high scores...");
        LootLockerSDKManager.GetScoreListMain(leaderboardID, 10, 0, (scoreListResponse) =>
        {
            if (scoreListResponse.success)
            {
                string tempPlayerNames = "Player\n";
                string tempPlayerScores = "Score\n";

                LootLockerLeaderboardMember[] members = scoreListResponse.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
                Debug.Log("Fetched high scores successfully");
                Debug.Log(tempPlayerNames);
                Debug.Log(tempPlayerScores);
            }
            else
            {
                Debug.Log("Failed: " + scoreListResponse.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => !done);
        Debug.Log("Fetch high scores routine completed");
    } }
