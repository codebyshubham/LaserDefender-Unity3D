using UnityEngine;
using System.Collections;

public class PlayerFormation : MonoBehaviour {

	public GameObject PlayerPrefab;
	public int playerlife = 3;

	// Use this for initialization
	void Start () {
		initplayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void spawnPlayer(){
		if(playerlife <= 1){
//			Debug.Log("game over");
			LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
			man.LevelLoad("win screen");
		}else{
			HealthSprite lives = GameObject.Find("life").GetComponent<HealthSprite>();
			lives.LoadSprites();
			initplayer();
			playerlife--;
		}
	}
	
	void initplayer(){
		foreach(Transform playerpos in transform){
			GameObject player = Instantiate(PlayerPrefab, playerpos.transform.position, Quaternion.identity) as GameObject;
			player.transform.parent = playerpos;
		}
	}
}
