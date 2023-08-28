using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using System.Collections;

public class messageSystem : MonoBehaviour
{
    public Text textBox;
    public Text titleBox;

    public void DisplayMessage(string title, string messageRaw)
    {
        titleBox.text = title;
        textBox.text = messageRaw;
    }
    

    public void checkForMessages()
    {
        LootLockerSDKManager.GetMessages((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved messages");
                if (response.messages != null && response.messages.Length > 0)
                {
                    string rawMessage = response.messages[0].body;
                    string title = response.messages[0].title;
                    DisplayMessage(title, rawMessage);
                }
            }
            else
            {
                Debug.Log("Error retrieving messages");
            }
        });
    }

    void Start()
    {
        StartCoroutine(passiveMe(2));
    }


    IEnumerator passiveMe(int secs)
    {
        yield return new WaitForSeconds(secs);
        checkForMessages();
    }
}
