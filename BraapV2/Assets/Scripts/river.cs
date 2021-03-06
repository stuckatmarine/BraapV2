using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class river : MonoBehaviour {

	public GameObject sled;
	public float radius;
	public bool riverCleared = false;
	public GameObject terrainObj;
	public float distanceForTrigger;
    public float distanceForWater;
    private bool firstWater = false;

	void Update(){

		if (this.transform.position.y - sled.transform.position.y < distanceForTrigger && riverCleared == false) {
			removeTrees ();
		//	Debug.Log ("trees removed");
			riverCleared = true;
		}

		if (this.transform.position.y - sled.transform.position.y < -10f && riverCleared == true) {
			riverCleared = false;
		}

       /* if (this.transform.position.y - sled.transform.position.y < distanceForWater)
        {
            sled.GetComponent<sledController>().bWater = true;
            firstWater = true;
        }

        if (this.transform.position.y - sled.transform.position.y > distanceForWater && firstWater)
            sled.GetComponent<sledController>().bWater = false;*/
    }


	public void removeTrees(){
		
		foreach (GameObject tree in terrainObj.GetComponent<treesAndSnow>().treeArr){

			if (Mathf.Abs (this.transform.position.y - tree.transform.position.y) < radius) {
				tree.transform.position = new Vector3 (200f, tree.transform.position.y, tree.transform.position.z);
			//	Debug.Log ("moving tree");
			}
		}


	//	Debug.Log ("stuff");
	/*	Collider[] riverTrees = Physics.OverlapSphere (this.transform.position, radius);
		int i = 0;
		Debug.Log ("removal iteration" + i);
		while (i < riverTrees.Length) {
			if (riverTrees[i].tag == "obstacle") {
				riverTrees [i].transform.position = new Vector3 (200f, riverTrees [i].transform.position.y, riverTrees [i].transform.position.z);
				i++;
				Debug.Log ("moved tree off river");
			}
		}*/
	}

	void OnTriggerEnter2D(Collider2D other) {
        sled.GetComponent<sledController>().bWater = true;
	}

	void OnTriggerExit2D(Collider2D other) {
        sled.GetComponent<sledController>().bWater = false;
	}
}
