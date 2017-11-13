using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_Application : MonoBehaviour {

    //For graceful exit
    public void QuitApplication()
    {
        Application.Quit();
    }
}
