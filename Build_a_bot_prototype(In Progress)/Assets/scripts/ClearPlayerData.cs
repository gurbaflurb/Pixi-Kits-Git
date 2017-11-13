using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clear Player data at Title and Load Screens
/// </summary>
public class ClearPlayerData : MonoBehaviour {

    List<int> tempList = new List<int>();

	// Use this for initialization
	void Start () {
        tempList.Add(-1);
        PlayerData.currentPlayer.Name = "Default";
        PlayerData.currentPlayer.ObjectsRewarded = tempList;
	}
}
