using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {
	
	// Update is called once per frame
	public void ChangeScene(string sceneToChangeTo) {
	
		Application.LoadLevel(sceneToChangeTo);
		
	}
	
}