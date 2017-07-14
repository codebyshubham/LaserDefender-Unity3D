using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;
	// Use this for initialization
	void Awake () {
		if(instance != null){
			Destroy(gameObject);
			//print("Music self-destructing");
		}else{
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
}
