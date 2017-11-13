using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour {

  [SerializeField]
  GameObject cannonPrefab;
  Vector2 currentPosition;
  Vector2 offset;
  Rigidbody2D rgbd;
  const int DESPAWN_TIME = 120;
  int timer;

	// Use this for initialization
	void Start () {
    currentPosition = this.transform.position;
    offset = new Vector2(0, 1.19f);
    rgbd = GetComponent<Rigidbody2D>();
    timer = 0;
;	}
	
	// Update is called once per frame
	void Update () {
		
    //Not moving and not in goal
    if(rgbd.velocity == Vector2.zero)
    {
      //if not moving for despawn time spawn cannon
      if(timer >= DESPAWN_TIME)
      {
        //grab current opsition
        currentPosition = this.transform.position;
        //Spawn Cannon in place
        Instantiate(cannonPrefab, currentPosition+offset, new Quaternion(0, 0, 0, 0));
        //Delete Dummy
        Destroy(this.gameObject);
      }
      //increment timer
      timer++;
    }
	}
}
