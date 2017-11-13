using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// handler for Play button being pressed on title screen
/// </summary>
public class Scene_Switcher : MonoBehaviour {

    //this code changes the scene to the search minigame scene
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
