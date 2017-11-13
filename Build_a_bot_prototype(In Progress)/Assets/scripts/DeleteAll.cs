using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DeleteAll : MonoBehaviour {
    BinaryFormatter bf = new BinaryFormatter();
    List<string> names;

    /// <summary>
    /// The DeleteAll button on the load screen is for debugging purposes only!
    /// Remove the button and this scripts before shipping game, lol
    /// </summary>
    public void Delete () {
        if(LoadPlayerList() == null) { return; }
        names = LoadPlayerList();

        //delete each player's data file
        foreach (string name in names)
        {
            if (File.Exists(Application.persistentDataPath + "/" + name+".dat"))
            {
                Debug.Log(Application.persistentDataPath + "/" + name+" to be deleted");
                File.Delete(Application.persistentDataPath + "/" + name+".dat");
            }
        }
        //now delete the playerlist file
        File.Delete(Application.persistentDataPath + "/playerList.dat");

        //Reload load screen
        Scene_Switcher newScene = gameObject.AddComponent<Scene_Switcher>();
        newScene.ChangeScene("loadGame");
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
}
