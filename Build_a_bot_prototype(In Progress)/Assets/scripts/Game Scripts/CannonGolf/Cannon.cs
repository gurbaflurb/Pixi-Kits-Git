using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

  public const float MAX_LENGTH = 5f;

  //Get audio source component
  AudioSource audioSource;

  //Acess the component that draws a line after click
  LineCreation lineCreation;
  public bool followMouse;

  //Mouse position for power and angle recordings
  public Vector2 mousePosition;
  public float distance;
  public Vector2 lastPosition;
  public Vector2 cannonPosition;

  //Dummy prefab
  [SerializeField]
  GameObject dummy;

  // Use this for initialization
  void Start () {
    audioSource = GetComponent<AudioSource>();
    lineCreation = GetComponent<LineCreation>();
    followMouse = false;
    mousePosition = Vector2.zero;
    lastPosition = mousePosition;
    cannonPosition = GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	void Update () {
		
    if(followMouse)
    {
      //Grabs mouses position in the world
      mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      //Check the distance to see if it's too far
      distance = Vector2.Distance(transform.position, mousePosition);

      //DISTANCE IS TO GREAT
      if (distance > MAX_LENGTH)
      {
        Debug.Log("TOO FAR!");
        //TODO: LATER CHANGE IT TO JUST NORMALIZE TO MAX POSITION?
        //use last known OK position
        mousePosition = lastPosition;
      }
      lastPosition = mousePosition;
      //Draws Line to mouse position
      lineCreation.Draw(mousePosition);
    }
	}

  //OnMouseDown
  //Follow Player's mouse and point at it
  //Record the Vectory (direction)
  //Based on how far the user has dragged their finger/mouse from the target increase power
  //display as a bar below the cannon
  private void OnMouseDown()
  {
    followMouse = true;
  }
  //OnMouseRelase
  //no longer record users finger?
  //Display the arrow of current vector
  //Display the powerbar
  //Spawn "Fire Button"
  public void OnMouseUp()
  {
    followMouse = false;
    //activate powerbar
    //activate fire button
    Fire();
  }

  public void Fire()
  {
    var obj = Instantiate(dummy, transform.position, transform.rotation);
    obj.GetComponent<Rigidbody2D>().AddForce(-(cannonPosition - mousePosition) * (distance * 50));
    audioSource.Play();
    Destroy(this.gameObject,audioSource.clip.length);
  }
}
