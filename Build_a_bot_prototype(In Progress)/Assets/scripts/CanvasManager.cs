using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class toggles the Search UI on/off
/// Does not allow toggling during Question and Answer sequence
/// </summary>
public class CanvasManager : MonoBehaviour {

    [SerializeField]
    GameObject menu;

    private bool isShowing = true;
	
	void Update () {
        if (Input.GetKeyDown("escape") && GameObject.FindGameObjectWithTag("QuestionAnswer") == null)
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
	}
}
