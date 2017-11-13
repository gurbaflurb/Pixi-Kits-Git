using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopulateScrollBox : MonoBehaviour {

    List<string> listOfPlayers = new List<string>();
    BinaryFormatter bf = new BinaryFormatter();
    GameObject playerButton;

    [SerializeField]
    GameObject button;

    //this is a prefab panel that alerts the player when there are no saved games to load
    [SerializeField]
    GameObject MakeNewSavedGame;
    
    // Use this for initialization
    void Start () {
        if (LoadPlayerList() == null) {
            HandleEmptyList();
            return;
        }
        listOfPlayers = LoadPlayerList();
        int playerNumber = listOfPlayers.Count;
        for (int counter = 0; counter < playerNumber; counter++)
        {
            playerButton = Instantiate(button);
            playerButton.GetComponentInChildren<Text>().text = listOfPlayers[counter];
            playerButton.transform.SetParent(GameObject.FindGameObjectWithTag("PlayerListScroll").transform, false);
            playerButton.GetComponent<Button>().onClick.AddListener(() => { OnButtonClicked(); });
        }
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
    void HandleEmptyList()
    {
        //Alert the player that no saved games exist
        Instantiate(MakeNewSavedGame).transform.SetParent(GameObject.FindGameObjectWithTag("ScrollBar").transform, false);
    }
    void OnButtonClicked()
    {
        string playerName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        LoadPlayerData(playerName);
        NextScene();
    }
    private void LoadPlayerData(string playerName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + playerName+".dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/" + playerName+".dat", FileMode.Open);
            PlayerDataToSerialize player = (PlayerDataToSerialize)bf.Deserialize(file);
            file.Close();
            PlayerData.currentPlayer.Name = player.Name;
            PlayerData.currentPlayer.ObjectsRewarded = player.ObjectsRewarded;
        }
        else
        {
            PlayerData.currentPlayer.Name = "Default";
            PlayerData.currentPlayer.ObjectsRewarded[0] = -1;
            return;
        }
    }
    //Now call the new scene loader script and begin the search scene
    private void NextScene()
    {
        Scene_Switcher newScene = gameObject.AddComponent<Scene_Switcher>();
        newScene.ChangeScene("scene1");
    }

}
