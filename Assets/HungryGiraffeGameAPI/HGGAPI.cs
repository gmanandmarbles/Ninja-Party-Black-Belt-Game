using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HGGAPI : MonoBehaviour
{
    private const string baseUrl = "http://localhost:3000"; // Change this to your API server URL

    public IEnumerator FetchMissions(Action<List<Mission>> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + "/api/missions"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching missions: " + www.error);
            }
            else
            {
                List<Mission> missions = JsonUtility.FromJson<List<Mission>>(www.downloadHandler.text);
                callback?.Invoke(missions);
            }
        }
    }

    public IEnumerator FetchBounties(Action<List<Bounty>> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + "/api/bounties"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching bounties: " + www.error);
            }
            else
            {
                List<Bounty> bounties = JsonUtility.FromJson<List<Bounty>>(www.downloadHandler.text);
                callback?.Invoke(bounties);
            }
        }
    }

    public IEnumerator FetchLeaderboards(Action<List<User>> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + "/api/leaderboards"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching leaderboards: " + www.error);
            }
            else
            {
                List<User> users = JsonUtility.FromJson<List<User>>(www.downloadHandler.text);
                callback?.Invoke(users);
            }
        }
    }

    public IEnumerator FetchUpcomingMissions(Action<List<Mission>> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + "/api/upcoming-missions"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching upcoming missions: " + www.error);
            }
            else
            {
                List<Mission> missions = JsonUtility.FromJson<List<Mission>>(www.downloadHandler.text);
                callback?.Invoke(missions);
            }
        }
    }

    public IEnumerator RegisterUser(UserRegistrationData userData, Action<string> callback)
    {
        string jsonData = JsonUtility.ToJson(userData);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/api/users", jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error registering user: " + www.error);
            }
            else
            {
                string responseMessage = www.downloadHandler.text;
                callback?.Invoke(responseMessage);
            }
        }
    }

    public IEnumerator LoginUser(UserLoginData loginData, Action<string> callback)
    {
        string jsonData = JsonUtility.ToJson(loginData);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/api/login", jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error logging in user: " + www.error);
            }
            else
            {
                string responseMessage = www.downloadHandler.text;
                callback?.Invoke(responseMessage);
            }
        }
    }

    public IEnumerator FetchUserProfile(string userId, Action<User> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + $"/api/users/{userId}"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching user profile: " + www.error);
            }
            else
            {
                User user = JsonUtility.FromJson<User>(www.downloadHandler.text);
                callback?.Invoke(user);
            }
        }
    }

    public IEnumerator SearchMissions(string query, Action<List<Mission>> callback)
    {
        string url = baseUrl + $"/api/search?query={Uri.EscapeDataString(query)}";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error searching missions: " + www.error);
            }
            else
            {
                List<Mission> missions = JsonUtility.FromJson<List<Mission>>(www.downloadHandler.text);
                callback?.Invoke(missions);
            }
        }
    }

    public IEnumerator AddMissionToOngoingList(string missionId, string playerId, Action<string> callback)
    {
        MissionPlayerData data = new MissionPlayerData { missionId = missionId, playerId = playerId };
        string jsonData = JsonUtility.ToJson(data);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/api/addmission", jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error adding mission to ongoing list: " + www.error);
            }
            else
            {
                string responseMessage = www.downloadHandler.text;
                callback?.Invoke(responseMessage);
            }
        }
    }

    public IEnumerator CompleteMission(string missionId, string playerId, Action<string> callback)
    {
        MissionPlayerData data = new MissionPlayerData { missionId = missionId, playerId = playerId };
        string jsonData = JsonUtility.ToJson(data);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/api/completemission", jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error completing mission: " + www.error);
            }
            else
            {
                string responseMessage = www.downloadHandler.text;
                callback?.Invoke(responseMessage);
            }
        }
    }

    public IEnumerator AddNewMission(Mission newMission, Action<Mission> callback)
    {
        string jsonData = JsonUtility.ToJson(newMission);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/api/missions", jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error adding new mission: " + www.error);
            }
            else
            {
                Mission createdMission = JsonUtility.FromJson<Mission>(www.downloadHandler.text);
                callback?.Invoke(createdMission);
            }
        }
    }

    public IEnumerator UpdateLeaderboards(List<LeaderboardEntry> entries, Action<string> callback)
    {
        string jsonData = JsonUtility.ToJson(entries);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/api/leaderboards", jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error updating leaderboards: " + www.error);
            }
            else
            {
                string responseMessage = www.downloadHandler.text;
                callback?.Invoke(responseMessage);
            }
        }
    }

    public IEnumerator AddUpcomingMission(Mission newMission, Action<Mission> callback)
    {
        string jsonData = JsonUtility.ToJson(newMission);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/api/upcoming-missions", jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error adding upcoming mission: " + www.error);
            }
            else
            {
                Mission createdMission = JsonUtility.FromJson<Mission>(www.downloadHandler.text);
                callback?.Invoke(createdMission);
            }
        }
    }

    [Serializable]
    public class Mission
    {
        public string title;
        public string description;
        // Add more fields as needed
    }

    [Serializable]
    public class Bounty
    {
        // Define bounty fields
    }

    [Serializable]
    public class User
    {
        // Define user profile fields
    }

    [Serializable]
    public class UserRegistrationData
    {
        public string username;
        public string password;
        // Add more fields as needed
    }

    [Serializable]
    public class UserLoginData
    {
        public string username;
        public string password;
    }

    [Serializable]
    public class MissionPlayerData
    {
        public string missionId;
        public string playerId;
    }

    [Serializable]
    public class LeaderboardEntry
    {
        // Define leaderboard entry fields
    }
}
