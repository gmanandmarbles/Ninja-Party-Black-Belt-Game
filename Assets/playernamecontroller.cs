using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;

public class playernamecontroller : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;
    [SerializeField] private Text statusText; // Reference to the Text object for status updates
    private bool isSessionActive = false;

    private void Start()
    {
        LootLockerSDKManager.CheckWhiteLabelSession(response =>
        {
            isSessionActive = response;
            if (isSessionActive)
            {
                // Start a new session
                Debug.Log("Session is valid. You can start a game session");
            }
            else
            {
                // Show login form here
                Debug.Log("Session is NOT valid. We should show the login form");
                LootLockerSDKManager.StartGuestSession((guestResponse) =>
                {
                    if (!guestResponse.success)
                    {
                        Debug.Log("Error starting LootLocker session");
                        return;
                    }

                    isSessionActive = true;
                    Debug.Log("Successfully started LootLocker session");
                });
            }
        });
    }

    public void SetPlayerName()
    {
        if (!isSessionActive)
        {
            Debug.Log("LootLocker session is not active. Please wait for the session to start.");
            return;
        }

        string playerName = nameInputField.text;

        if (playerName.Length >= 12)
        {
            statusText.text = "Username must be 11 characters or less. Please choose a shorter username.";
            return;
        }

        LootLockerSDKManager.SetPlayerName(playerName, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
                statusText.text = "Player name set successfully.";
            }
            else
            {
                Debug.Log("Error setting player name");
                statusText.text = "Error setting player name.";
            }
        });
    }

    public void GetPlayerName()
    {
        if (!isSessionActive)
        {
            Debug.Log("LootLocker session is not active. Please wait for the session to start.");
            return;
        }

        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player name: " + response.name);
                statusText.text = "Player name: " + response.name;
            }
            else
            {
                Debug.Log("Error getting player name");
                statusText.text = "Error getting player name.";
            }
        });
    }
}
