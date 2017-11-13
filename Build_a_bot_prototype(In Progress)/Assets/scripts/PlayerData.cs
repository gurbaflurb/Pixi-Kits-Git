using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Data container for the player's information
/// Singleton design pattern
/// </summary>
/// 
class PlayerData : MonoBehaviour
{
    private string name;
    private List<int> objectsRewarded = new List<int>();

    public static PlayerData currentPlayer;

    void Awake ()
    {
        if(currentPlayer != null)
        {
            Destroy(this.gameObject);
            return;
        }
        currentPlayer = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    public PlayerData()
    {
        name = "Default";
    } 
    public string Name
    {
       set { name = value; }
       get { return name; } 
    }
    public List<int> ObjectsRewarded
    {
        set { objectsRewarded = value; }
        get { return objectsRewarded; }
    }

    public void AddObject(int objectToAdd)
    {
        //Need to add the code that will detect the default game object
        if (objectToAdd != -1 && objectsRewarded[0] == -1)
        {
            objectsRewarded.Remove(-1);
        }
        objectsRewarded.Add(objectToAdd);
    }
}

[Serializable]
class PlayerDataToSerialize 
{
    private string name;
    private List<int> objectsRewarded;

    public string Name
    {
        set { name = value; }
        get { return name;  }
    }
    public List<int> ObjectsRewarded
    {
        set { objectsRewarded = value; }
        get { return objectsRewarded; }
    }
}

