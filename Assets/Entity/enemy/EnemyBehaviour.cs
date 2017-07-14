using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public float projectileSpeed = 10;
	public float health = 150f;
	public float firePerSecond = 0.5f;
	public int ScoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	private FormationController formationcontroller;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		formationcontroller = GameObject.FindObjectOfType<FormationController>();
	}
	
	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile){
			missile.Hit();
			health -= missile.GetDamage();
			if(health <= 0){
				AudioSource.PlayClipAtPoint(deathSound, transform.position);
				Destroy(gameObject);
				formationcontroller.EnemyDestroyed();
				scoreKeeper.Score(ScoreValue);
			}
		}
	}
	
	void Update () {
		float probability = Time.deltaTime * firePerSecond;
		if(Random.value < probability){
			Fire();
		}
			
	}
	
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0,-1,0);
		GameObject missile= Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
}
