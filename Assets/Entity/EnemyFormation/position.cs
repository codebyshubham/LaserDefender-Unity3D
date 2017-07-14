using UnityEngine;
using System.Collections;

public class position : MonoBehaviour {

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}
