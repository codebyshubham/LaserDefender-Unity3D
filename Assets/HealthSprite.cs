using UnityEngine;
using System.Collections;

public class HealthSprite : MonoBehaviour {

	public Sprite[] healthSprite;
	
	private int spriteIndex = 0;
	
	void Start () {
		LoadSprites();
	}
	
	public void LoadSprites(){
		if(healthSprite.Length >= spriteIndex){
		//	print("spirites block enter");
			this.GetComponent<SpriteRenderer>().sprite = healthSprite[spriteIndex];
		}
		spriteIndex++;
	}
}
