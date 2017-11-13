using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class nextButtonScript : MonoBehaviour {

    [SerializeField]
    InputField inputField;

    BinaryFormatter bf = new BinaryFormatter();
    string inputFieldtext;

    public void NextButtonPressed()
    {
        inputFieldtext = inputField.text;
        if (inputFieldtext == null || inputFieldtext == "" || inputFieldtext == inputField.placeholder.GetComponent<Text>().text)
        {
            return;
        }
        if (!File.Exists(Application.persistentDataPath+"/playerList.dat")) { CreateNewPlayerListFile(); }
        List<string> playerList = LoadPlayerList();
        if (CheckForDuplicates(playerList)) { return; } 
        if (playerList[0] == "Default") { playerList.Remove("Default"); }
        playerList.Add(inputFieldtext);
        SavePlayerList(playerList);
        CreateNewPlayerFile();

        //Now call the new scene loader script and begin the search scene
        Scene_Switcher newScene = gameObject.AddComponent<Scene_Switcher>();
        newScene.ChangeScene("scene1");
    }
   private List<string> LoadPlayerList()
    {
        if (File.Exists(Application.persistentDataPath + "/playerList.dat"))
        {      
            FileStream file = File.Open(Application.persistentDataPath + "/playerList.dat", FileMode.Open);
            List<string> playerList = (List<string>)bf.Deserialize(file);
            file.Close();
            return playerList;
        }
        else
        {
            return null;
        }
    }
    private void SavePlayerList(List<string> playerList)
    {
        FileStream file = File.Open(Application.persistentDataPath + "/playerList.dat", FileMode.Open);
        bf.Serialize(file, playerList);
        file.Close();
    }
    private void CreateNewPlayerListFile()
    {
        FileStream file = File.Open(Application.persistentDataPath + "/playerList.dat", FileMode.Create);
        List<string> playerList = new List<string>();
        playerList.Add("Default");
        bf.Serialize(file, playerList);
        file.Close();
    }
    private bool CheckForDuplicates(List<string> playerList)
    {
        foreach (string player in playerList)
        {
            if (player == inputFieldtext)
            {
                inputField.text = "";
                inputField.placeholder.GetComponent<Text>().text = "Name taken";
                return true;
            }
        }
        return false;
    }
    private void CreateNewPlayerFile()
    {
        //Add current players data to PlayerData singleton class
        //A value of -1 in position 0 indicates a newly created player file
        //and the player has not yet found any objects
        PlayerData.currentPlayer.Name = inputFieldtext;
        PlayerData.currentPlayer.AddObject(-1);
        SavePlayerData();      
    }
    public void SavePlayerData()
    {
        //Create instance of serializable player data container and populate with info
        PlayerDataToSerialize currPlayer = new PlayerDataToSerialize();
        currPlayer.Name = PlayerData.currentPlayer.Name;
        currPlayer.ObjectsRewarded = PlayerData.currentPlayer.ObjectsRewarded;

        //write to user's file
        FileStream file = File.Open(Application.persistentDataPath + "/" + PlayerData.currentPlayer.Name+".dat", FileMode.Create);
        bf.Serialize(file, currPlayer);
        file.Close();
    }
}
