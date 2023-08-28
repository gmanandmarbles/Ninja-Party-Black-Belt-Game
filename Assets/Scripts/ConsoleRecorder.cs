using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LootLocker.Requests;

public class ConsoleRecorder : MonoBehaviour
{
//     private static Queue<string> consoleMessages = new Queue<string>();
//     private static DateTime lastSaveTime;
//     private int playerFileID = 0; // Initialize with the appropriate player file ID

//     void Start()
//     {
//         Application.logMessageReceived += HandleLog;
//         lastSaveTime = DateTime.Now;

//         // Start a background thread to continuously save console data
//         StartCoroutine(SaveLogData());
//     }

//     void HandleLog(string logString, string stackTrace, LogType type)
//     {
//         string message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - [{type}] - {logString}";
//         consoleMessages.Enqueue(message);
//     }

//     IEnumerator SaveLogData()
//     {
//         while (true)
//         {
//             yield return new WaitForSeconds(5.0f); // Adjust the interval as needed

//             if (consoleMessages.Count > 0 || IsNewDay())
//             {
//                 List<string> messages = new List<string>(consoleMessages);
//                 consoleMessages.Clear();

//                 string jsonData = JsonUtility.ToJson(messages, true);

//                 string filePath = Application.persistentDataPath + "/console_logs.json";
//                 File.WriteAllText(filePath, jsonData);

//                 // Read the JSON file as bytes and upload using LootLocker SDK
//                 byte[] fileBytes = File.ReadAllBytes(filePath);

//                 if (playerFileID == 0)
//                 {
//                     CreatePlayerFile(fileBytes);
//                 }
//                 else
//                 {
//                     UpdatePlayerFile(playerFileID, fileBytes);
//                 }

//                 lastSaveTime = DateTime.Now;
//             }
//         }
//     }

//     bool IsNewDay()
//     {
//         return DateTime.Now.Date > lastSaveTime.Date;
//     }

//     void OnDestroy()
//     {
//         Application.logMessageReceived -= HandleLog;
//     }

//     void CreatePlayerFile(byte[] fileBytes)
//     {
//         var file = File.Open("/path/to/file/save_game.zip"", FileMode.Open);
//     var fileBytes = new byte[file.Length];
// file.Read(fileBytes, 0, Convert.ToInt32(file.Length));

// LootLockerSDKManager.UploadPlayerFile(fileBytes, "filename", "save_game", response =>
// {
//     if (response.success)
//     {
//         Debug.Log("Successfully uploaded player file, url: " + response.url);
//     } 
//     else
//     {
//         Debug.Log("Error uploading player file");
//     }
// });
//     }

//     void UpdatePlayerFile(int fileID, byte[] fileBytes)
//     {
//     LootLockerSDKManager.UpdatePlayerFile(playerFileID, fileBytes, response =>
//     {
//         if (response.success)
//         {
//             Debug.Log("Successfully updated player file, url: " + response.url);
//         } 
//         else
//         {
//             Debug.Log("Error updating player file");
//         }
//     });
//     }
}
