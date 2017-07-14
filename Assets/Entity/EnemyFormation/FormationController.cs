using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10;
	public float height = 5;
	public float speed = 5.0f;
	public float padding = 1f;
	public float spawnDelaySecond = 1f;
	public int totalEnemy;
	
	private int direction = 1;
	private float boundaryRightEdge,boundaryLeftEdge;
	private LevelManager levelManager;
	
	void Start () {
		
		levelManager = GameObject.FindObjectOfType<LevelManager>();
			
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		boundaryLeftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
		boundaryRightEdge = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;
		
		spawnEnemies();
	}
	
	
	
	void OnDrawGizmos(){
		float xmin,xmax,ymin,ymax;
		xmin = transform.position.x - 0.5f * width;
		xmax = transform.position.x + 0.5f * width;
		ymin = transform.position.y - 0.5f * height;
		ymax = transform.position.y + 0.5f * height;
		Gizmos.DrawLine(new Vector3(xmin, ymin, 0), new Vector3(xmin, ymax, 0));
		Gizmos.DrawLine(new Vector3(xmin, ymax, 0), new Vector3(xmax, ymax, 0));
		Gizmos.DrawLine(new Vector3(xmax, ymax, 0), new Vector3(xmax, ymin, 0));
		Gizmos.DrawLine(new Vector3(xmax, ymin, 0), new Vector3(xmin, ymin, 0));
	}
	
	
	void Update () {
		float formationRightEdge = transform.position.x + 0.5f * width;
		float formationLeftEdge = transform.position.x - 0.5f * width;
		if(formationRightEdge > boundaryRightEdge){
			direction = -1;
		}
		if(formationLeftEdge < boundaryLeftEdge){
			direction = 1;
		}
		transform.position += new Vector3(direction * speed * Time.deltaTime,0,0);
		
		if(AllMembersAreDead()){
			if(totalEnemy <= 0){
				levelManager.LoadNextLevel();
			}else{
				SpawnUntilFull();
			}
		}
	}
	
	public void EnemyDestroyed(){
		totalEnemy--;
	}
	 
	void spawnEnemies(){
		foreach(Transform position in transform){
			GameObject enemy = Instantiate(enemyPrefab, position.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = position;
		}
	}
	
	void SpawnUntilFull(){
		Transform freePos = NextFreePosition();
		GameObject enemy = Instantiate(enemyPrefab, freePos.position, Quaternion.identity) as GameObject;
		enemy.transform.parent = freePos;
		if(FreePositionExist()){
			Invoke("SpawnUntilFull", spawnDelaySecond);
		}
	}
	
	Transform NextFreePosition(){
		foreach(Transform position in transform){
			if(position.childCount <= 0){
				return position;
			}
		}
		return null;
	}
	
	bool FreePositionExist(){
		foreach(Transform position in transform){
			if(position.childCount <= 0){
				return true;
			}
		}
		return false;
	}
	
	bool AllMembersAreDead(){
		foreach(Transform position in transform){
			if(position.childCount > 0){
				return false;
			}
		}
		return true;
	}
}




