using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Obtained from http://answers.unity3d.com/questions/160106/implementing-a-pseudo-pedometer.html
/// Posted by: aldonaletto
/// </summary>
public class WalkSpawn : MonoBehaviour {
    private float loLim = 0.005F;
    private float hiLim = 0.3F;
    private int steps = 0;
    private bool stateH = false;
    private float fHigh = 8.0F;
    private float curAcc = 0F;
    private float fLow = 0.2F;
    private float avgAcc;
    private float spawnTimer = 15;
    private int travel = 5;
    private int currSteps;

    [SerializeField]
    GameObject chest;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currSteps = stepDetector();

        if (currSteps >= travel)
        {
            currSteps = 0;
            steps = 0;
            Chest();
        } 

}
    public int stepDetector()
    {
        curAcc = Mathf.Lerp(curAcc, Input.acceleration.magnitude, Time.deltaTime * fHigh);
        avgAcc = Mathf.Lerp(avgAcc, Input.acceleration.magnitude, Time.deltaTime * fLow);
        float delta = curAcc - avgAcc;
        if (!stateH)
        {
            if (delta > hiLim)
            {
                stateH = true;
                steps++;
            }
            else if (delta < loLim)
            {
                stateH = false;
            }
            stateH = false;
        }
        avgAcc = curAcc;

        return steps;
    }

    public void Chest()
    {
        if (!GameObject.FindGameObjectWithTag("TreasureChest"))
        {
            Instantiate(chest);
        }

    }

}
