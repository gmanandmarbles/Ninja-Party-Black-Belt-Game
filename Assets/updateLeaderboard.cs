using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LootLocker.Requests;

public class UpdateLeaderboard : MonoBehaviour
{
    private int memberID = 0;
    private int leaderboardID = 14540;
    private string playerName = "Hahaman"; // Default player name

    // Start is called before the first frame update
    void Start()
    {
        memberID = PlayerPrefs.GetInt("member");
        
        // Fetch player's name from the endpoint
        StartCoroutine(FetchPlayerName(memberID));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FetchPlayerName(int userId)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"https://api.hungrygiraffe.xyz/api/users/{userId}"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Fetching Player Name Error: " + www.error);
            }
            else
            {
                // Parse the response JSON to extract player name
                string jsonResponse = www.downloadHandler.text;
                playerName = ParsePlayerNameFromResponse(jsonResponse);
                Debug.Log("Fetched Player Name: " + playerName);

                // After fetching player's name, you can now use it for updating the leaderboard
                LeaderboardUpdate(100); // Example score value
            }
        }
    }

    public void LeaderboardUpdate(int score)
    {
        // Upload to hungrygiraffe API
        StartCoroutine(Upload(playerName, memberID, score));

        // Submit score to LootLocker leaderboard
        LootLockerSDKManager.SubmitScore(memberID.ToString(), score, leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("LootLocker Successful");
            }
            else
            {
                Debug.Log("LootLocker Failed: " + response.Error);
            }
        });
    }

    IEnumerator Upload(string name, int id, int score)
    {
        // Construct JSON data
        string jsonData = $"{{ \"name\":\"{name}\",\"id\":{id},\"points\":{score},\"avatar\":\"https://raw.githubusercontent.com/CodeninjasWS/hungrygiraffestorage/main/noimage.jpeg\"}}";

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm("https://api.hungrygiraffe.xyz/api/leaderboards", jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("HungryGiraffe API Error: " + www.error);
            }
            else
            {
                Debug.Log("HungryGiraffe API upload complete!");
            }
        }
    }

    private string ParsePlayerNameFromResponse(string jsonResponse)
    {
        // Implement JSON parsing logic to extract the player name
        try
        {
            // Assuming you are using Unity's JsonUtility
            PlayerResponse response = JsonUtility.FromJson<PlayerResponse>(jsonResponse);
            return response.username;
        }
        catch (System.Exception e)
        {
            Debug.Log("JSON Parsing Error: " + e.Message);
            return "DefaultPlayerName";
        }
    }

    // Create a class to represent the JSON response structure
    [System.Serializable]
    private class PlayerResponse
    {
        public string id;
        public string username;
    }
}
