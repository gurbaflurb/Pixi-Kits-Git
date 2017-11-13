using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class rotates the main camera using the phone's built in gyro
/// </summary>
public class GyroCameraController : MonoBehaviour {

    GameObject camParent;

    // Use this for initialization
    void Start()
    {
        camParent = new GameObject("CamParent");
        camParent.transform.position = this.transform.position;
        this.transform.parent = camParent.transform;
        camParent.transform.Rotate(Vector3.right, 90);
        Input.gyro.enabled = true;
    }

    //Update is called once per frame
    void Update()
    {
        Quaternion rotFix = new Quaternion(Input.gyro.attitude.x,
                                            Input.gyro.attitude.y,
                                            -Input.gyro.attitude.z,
                                            -Input.gyro.attitude.w);
        this.transform.localRotation = rotFix;
    }
}
