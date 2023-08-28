using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;

public class WhiteLabelAuth : MonoBehaviour
{
    public InputField emailInputField;    // Reference to the email InputField component
    public InputField passwordInputField; // Reference to the password InputField component

    public string email;    // Variable to store the email
    public string password; // Variable to store the password

    public Text statusText;


    public void login()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        bool rememberMe = true;

        LootLockerSDKManager.WhiteLabelLoginAndStartSession(email, password, rememberMe, (response) =>
        {
            if (!response.success)
            {
                if (!response.LoginResponse.success)
                {
                    Debug.Log("Error while logging in");
                    statusText.text = "Error while logging in.";
                }
                else if (!response.SessionResponse.success)
                {
                    Debug.Log("Error while starting session");
                    statusText.text = "Error while starting session.";
                }
                return;
            }

            // Handle Returning Player

        });
    }

    public void register()
    {
        LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("error while creating user");
                statusText.text = "Error while creating user";
                return;
            }

            Debug.Log("user created successfully");
            statusText.text = "User created successfully, now click the login button. After clicking the login button go back to the main menu and start playing!";
        });
    }

    private void Start()
    {
        // Attach methods to handle the email and password value change events
        emailInputField.onValueChanged.AddListener(OnEmailValueChanged);
        passwordInputField.onValueChanged.AddListener(OnPasswordValueChanged);

        LootLockerSDKManager.CheckWhiteLabelSession(response =>
        {
            if (response)
            {
                // Start a new session
                Debug.Log("session is valid, you can start a game session");
                statusText.text = "Session is valid, you can start playing.";
            }
            else
            {
                // Show login form here
                Debug.Log("session is NOT valid, we should show the login form");
                statusText.text = "Session is not valid, please log in.";
            }
        });
    }

    private void OnEmailValueChanged(string value)
    {
        // Update the email variable with the current email input field value
        email = value;
        PlayerPrefs.SetString("email", email);
    }

    private void OnPasswordValueChanged(string value)
    {
        // Update the password variable with the current password input field value
        password = value;
        PlayerPrefs.SetString("password", password);
    }

    public void passReset()
    {
        // This code should be placed in a handler when the user clicks reset password.

        LootLockerSDKManager.WhiteLabelRequestPassword(email, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("error requesting password reset");
                statusText.text = "Error requesting password reset, try checking the email.";
                return;
            }

            Debug.Log("requested password reset successfully");
            statusText.text = "Password reset email sent successfully.";
        });
    }
}
