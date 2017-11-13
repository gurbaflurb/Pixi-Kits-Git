using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreation : MonoBehaviour {

  //Positions line endings
  public Transform Starting;
  public Transform Ending;

  //Grab the lineRenderer
  private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
    //Set Starting and ending points
    Starting = GetComponent<Transform>();
    Ending = Starting;

    //set lineRenderer point 1 and 2 (currently same spot)
    lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.SetPosition(0, Starting.position);
    lineRenderer.SetPosition(1, Ending.position);
	}

  public void Draw(Vector3 endingPosition)
  {
    lineRenderer.SetPosition(1, endingPosition);
  }

  public void Erase()
  {
    lineRenderer.SetPosition(1, Starting.position);
  }
}
