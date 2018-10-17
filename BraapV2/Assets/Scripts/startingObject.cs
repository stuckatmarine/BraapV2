using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingObject : MonoBehaviour {

	void Awake(){
		transform.position = new Vector3 (transform.position.x + Random.Range (-10f, 10f), transform.position.y + Random.Range (-5f, 4f), transform.position.z) ;
	
	}
}
