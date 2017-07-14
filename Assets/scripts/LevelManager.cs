using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LevelLoad(string name){
		Debug.Log("level load name : " + name);
		Application.LoadLevel(name);
	}
	
	public void QuitGame(){
		Debug.Log("quit working");
		Application.Quit();
	}
	
	public void LoadNextLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
}
