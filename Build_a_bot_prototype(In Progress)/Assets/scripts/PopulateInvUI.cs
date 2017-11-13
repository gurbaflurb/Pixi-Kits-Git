using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopulateInvUI : MonoBehaviour {

    //Get icon slots from Inventory UI
    Image[] parts;

    [SerializeField]
    GameObject[] icons;
    
	// Use this for initialization
	void Start () {

        parts = this.gameObject.GetComponentsInChildren<Image>();
        UpdateInventory();
	}

    public void UpdateInventory()
    {
        //check for an empty inventory (player has not yet found any objects)
        if (PlayerData.currentPlayer.ObjectsRewarded[0] == -1) { return; }

        //clear inventory contents
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].sprite = null;
        }
        //populate inventory
        for (int i = 0; i < PlayerData.currentPlayer.ObjectsRewarded.Count; i++)
        {
            parts[i].sprite = icons[PlayerData.currentPlayer.ObjectsRewarded[i]].GetComponentInChildren<SpriteRenderer>().sprite;
        }

      if(PlayerData.currentPlayer.ObjectsRewarded.Count >= icons.GetLength(0))
      {
      Debug.Log("All items retreived moving to build");
      SceneManager.LoadScene("ARto2D");
      }
    }
}

