using UnityEngine;
using System.Collections;

public class cameraStopTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
        GameObject.Find("sled").GetComponent<sledController>().Crash();
    }
}
