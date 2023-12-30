using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]
public class Message
{
    public string title;
    public string body;
}

[System.Serializable]
public class MessagesResponse
{
    public bool success;
    public Message[] messages;
}

public class messageSystem : MonoBehaviour
{
    public Text textBox;
    public Text titleBox;

    public void DisplayMessage(string title, string messageRaw)
    {
        titleBox.text = title;
        textBox.text = messageRaw;
    }

    public void CheckForMessages()
    {
        StartCoroutine(GetMessagesFromAPI());
    }

    IEnumerator GetMessagesFromAPI()
    {
        string apiUrl = "https://api.hungrygiraffe.xyz/api/messages";

        UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseJson = www.downloadHandler.text;
            
            // Parse the JSON response using JsonUtility
            MessagesResponse responseData = JsonUtility.FromJson<MessagesResponse>(responseJson);

            if (responseData.success && responseData.messages.Length > 0)
            {
                string rawMessage = responseData.messages[0].body;
                string title = responseData.messages[0].title;
                DisplayMessage(title, rawMessage);
            }
        }
        else
        {
            Debug.LogError("Error retrieving messages: " + www.error);
        }
    }

    void Start()
    {
        StartCoroutine(PassiveMe(2));
    }

    IEnumerator PassiveMe(int secs)
    {
        yield return new WaitForSeconds(secs);
        CheckForMessages();
    }
}
