using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPickup : MonoBehaviour {

    [SerializeField]
    GameObject prefabQuestionAnswer;

    [SerializeField]
    float speed = .01f;

    Vector3 chestPosition;
    Vector3 chestCapturePosition;
    bool secondLeg;
    bool finalLeg;
    GameObject chest;

	// Use this for initialization
	void Start () {

        chest = GameObject.FindGameObjectWithTag("TreasureChest");

        //ensure that a chest does exist
        //if no chest exists then this object will destroy itself
        if (chest == null) Destroy(this);

        //Get position of Chest in 3D space
        //The pickup object will use this position to navigate to pickup the chest
        chestPosition = chest.transform.position;
        chestPosition.x += 2; //just to the right of the chest
        chestPosition.z += 2; //behind the chest
        chestPosition.y += 1; //slightly above the chest
        secondLeg = false;
        finalLeg = false;
        chestCapturePosition = new Vector3(0, -1, 5);
    }
	
	// Move Chest Pickup object to grab the Treasure Chest
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, chestPosition, speed*Time.deltaTime);

        //Check to see if Pickup Object's 1st leg of the journey is complete
        if(Vector3.Distance(transform.position, chestPosition) < 1 && secondLeg == false)
        {
            chestPosition.x -= 2;
            secondLeg = true;
        }

        //Check to see if Pickup Object's 2nd leg of the journey is complete
        if (secondLeg == true && Vector3.Distance(transform.position, chestPosition) < 1)
        {
            finalLeg = true;
        }

        //Check to see if final leg is active, and if it is then bring both objects back to the player
        if (finalLeg == true)
        {
            chest.transform.position = 
                Vector3.MoveTowards(chest.transform.position,
                chestCapturePosition, speed);

            //Destroy this object 
            if(Vector3.Distance(chest.transform.position, chestCapturePosition) < 1)
            {
                Camera.main.transform.position = Vector3.zero;
                Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
                chest.transform.SetParent(Camera.main.transform);
                Instantiate(prefabQuestionAnswer);
                Destroy(gameObject);
            }
        }
	}
}
