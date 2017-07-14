using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1;
	public float projectileRepeatRate = 0.2f;
	
	public GameObject laser1;
	public GameObject laser2;
	public float projectileSpeed = 10;
	public float health = 300f;
	
	public AudioClip fireSound;
	
	private float xmin = -5;
	private float xmax = +5;
	
	private PlayerFormation formation;
	
	void Start () {
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
		xmax = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;
		
		formation = GameObject.Find("PlayerFormation").GetComponent<PlayerFormation>();
	}
	
	void Fire(){
		Vector3 offset = new Vector3(0,1,0);
		if(Application.loadedLevel == 5){
			GameObject beam = Instantiate(laser2, transform.position+offset, Quaternion.identity) as GameObject;
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
			AudioSource.PlayClipAtPoint(fireSound, transform.position);
		}else{
			GameObject beam = Instantiate(laser1, transform.position+offset, Quaternion.identity) as GameObject;
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
			AudioSource.PlayClipAtPoint(fireSound, transform.position);
		}
	}
	
	void Update () {	
		MobileControl();
		PcControl();
	}
	
	void PcControl(){
		if(Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.0001f, projectileRepeatRate);
		}
		if(Input.GetKeyUp(KeyCode.UpArrow)||Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x - speed * Time.deltaTime, xmin, xmax),
				transform.position.y,
				transform.position.z
				);
		}	
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x + speed * Time.deltaTime, xmin, xmax),
				transform.position.y,
				transform.position.z
				);
		}
	}
	
	void MobileControl(){
		Vector3 dir = Vector3.zero;
		dir.x = Input.acceleration.x;
		if (dir.sqrMagnitude > 1)
			dir.Normalize();		
		dir *= Time.deltaTime;
		transform.Translate (dir * speed);
		var pos = transform.position;
		pos.x =  Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = pos;
		
		if(Input.GetMouseButtonDown(0)){
			InvokeRepeating("Fire", 0.0001f, projectileRepeatRate);
		}
		
		if(Input.GetMouseButtonUp(0)){
			CancelInvoke("Fire");
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile){
			missile.Hit();
			health -= missile.GetDamage();
			if(health <= 0){
				Die();
			}
		}
	}
	
	
	void Die(){
		Destroy(gameObject);
		formation.spawnPlayer();
	}
}
