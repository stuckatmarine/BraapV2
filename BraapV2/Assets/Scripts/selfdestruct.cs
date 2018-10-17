using UnityEngine;
using System.Collections;

public class selfdestruct : MonoBehaviour {

public Transform target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (target.position.y >= 50.0)
			//Destroy(gameObject);
			gameObject.SetActive(false);
	}
}
