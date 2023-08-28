using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadyPlayerMe.Core;
using UnityEngine.UI;
using LootLocker.Requests;

public class locateAvatarAndSave : MonoBehaviour
{
    public string AvatarUrl;
    public InputField avatarFeild;
    private bool isSessionActive = false;
    private const string avatarKey = "AvatarKey"; // Add this line as a constant with an appropriate key

    void Start()
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
    void Update()
    {

    }

    public void AvatarFinder()
    {
        string AvatarUrl = avatarFeild.text;
        AvatarObjectLoader avatarLoader = new AvatarObjectLoader();
        avatarLoader.LoadAvatar(AvatarUrl); 
         PlayerPrefs.SetString(avatarKey, AvatarUrl);
        print(AvatarUrl);
        LootLockerSDKManager.UpdateOrCreateKeyValue("avatar", AvatarUrl, (getPersistentStoragResponse) =>
        {
            if (getPersistentStoragResponse.success)
            {
                Debug.Log("Successfully updated player storage");
            }
            else
            {
                Debug.Log("Error updating player storage");
            }
        });
    }
}
