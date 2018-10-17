using UnityEngine;
using System.Collections;

public class track : MonoBehaviour {

private bool space;
public float braapPower = 10f;

	void Start(){
	}
	void Update ()
	{
		if (Input.GetKey (KeyCode.D)) {
			space = true;
		} else {
			space = false;
		}
				
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		if (space) {
			rb.AddForce (transform.right * braapPower);
			Debug.Log("d");
		}
	}
}
