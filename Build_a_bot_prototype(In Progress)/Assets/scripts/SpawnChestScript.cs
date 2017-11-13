using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChestScript : MonoBehaviour {

    [SerializeField]
    GameObject chest;

    public void Chest()
    {
        if (!GameObject.FindGameObjectWithTag("TreasureChest"))
        {
            Instantiate(chest);
        }
    }
}
