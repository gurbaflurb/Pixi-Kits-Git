using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeController : MonoBehaviour {
    float rotateSpeed = 50;

    [SerializeField]
    GameObject prefabChestPickup;

    // Update is called once per frame
    void Update () {
        transform.Rotate(Vector3.down * (rotateSpeed * Time.deltaTime));
        }

    //Check if Chest is being clicked on
    private void OnMouseDown()
    {
        //ensure that no other ChestPickup object exists
        if (!GameObject.FindGameObjectWithTag("ChestPickup") && GameObject.FindGameObjectWithTag("QuestionAnswer") == null)
        {
            Instantiate(prefabChestPickup);
        }   
    }
}

