using UnityEngine;
using System.Collections;

public class treesAndSnow : MonoBehaviour {

public GameObject tree1Prefab;
public GameObject tree2;
public GameObject tree3;
public GameObject tree4;
public GameObject snowTile;
public Transform target;
public GameObject moose;
public GameObject riverTile;

public float xTolerance = 1f;
public float yTolerance = 1f;
public float yOffset = 1f;
public float treeSpawnRate = 1f;
public float timer = 40f;
public float sideSpawnRate = 10f;
public float tileSize = 100f;
public float snow1Offset = 29.12f;
public float mooseSpawnrate = .3f;
public float maxTreeSpawnRate = 40f;
public float sledFurthest = 0;


private float xLocation;
private float yLocation;
private bool treeSpawn;
private float random1000;
private float count = 0f;
private float sideCount = 0f;
private int obstacleNum;
private bool sideBool = false;
private int tileCount = 1;
private int treeCount = 23;
private int totalTrees = 55;
private int totalTiles = 6;
private int tileNum = 2;
private int riverRate;
public bool paused = true;
private bool riverSpawn = false;
private bool noDoubleRiver = true;

//array non garbage system
	public GameObject[] treeArr = new GameObject[40];
	public GameObject[] tileArr = new GameObject[6];

	void Start(){
		
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		sledFurthest = target.position.y;
		if (tileNum == 6)
			tileNum = 0;

		if (treeCount == totalTrees)
			treeCount = 0;
		
		if (!paused) {
			if (treeSpawnRate < maxTreeSpawnRate) {
				treeSpawnRate += 0.001f;
			}
			// ----------------------background spawns--------------------------

			if (target.position.y >= tileSize * tileCount - 100f) {
				if (target.position.y >= 150f && !riverSpawn) { //was 500---------------
					//GameObject river = Instantiate (riverTile, new Vector3 (-0.23f, 
					//	tileSize * tileCount + snow1Offset, 39f), Quaternion.identity) 
				//		as GameObject;
					riverTile.transform.position = new Vector3 (-0.239f, tileSize *( tileCount +1) + snow1Offset , 39f);
					tileCount += 3;
					riverSpawn = true;
					noDoubleRiver = false;
				} else if (target.position.y > 250f) {
					riverRate = Random.Range (1, 15);
					if (riverRate <= 1 && noDoubleRiver == true) {
						//GameObject river = Instantiate (riverTile, new Vector3 (-0.23f, 
							      //            tileSize * tileCount + snow1Offset, 39f), Quaternion.identity) 
					//as GameObject;
						riverTile.transform.position = new Vector3 (-0.239f, tileSize * tileCount + snow1Offset, 39f);
						tileCount += 2;
						noDoubleRiver = false;
					} else {
					//	GameObject snow = Instantiate (snowTile, new Vector3 (-0.23f, 
						//	                 tileSize * tileCount + snow1Offset, 39f), Quaternion.identity) 
					//	as GameObject;
						tileArr[tileNum].transform.position = new Vector3 (-0.239f, tileSize * tileCount + snow1Offset, 39f);
						tileCount++;
						tileNum++;
						noDoubleRiver = true;
					}
				} else {
					//GameObject snow = Instantiate (snowTile, new Vector3 (-0.23f, 
					//	                 tileSize * tileCount + snow1Offset, 39f), Quaternion.identity) 
					//as GameObject;
					tileArr[tileNum].transform.position = new Vector3 (-0.239f, tileSize * tileCount + snow1Offset, 39f);
					tileCount++;
					tileNum++;
					noDoubleRiver = true;
				}
			} 

			//-------------------obstacle spawns---------------
			if (target.GetComponent<sled> ().crashed == false) {
				random1000 = Random.Range (0f, 1000f);
				count++;
				sideCount++;
				//spawn trees on side
				if (sideSpawnRate > random1000) {
					leftTreeSpawner ();
					rightTreeSpawner ();
					sideCount = 0f;
				}
				if (treeSpawnRate > random1000) {
					randomTreeSpawner ();
				}
				//randrom tree spawn plu
				if (count >= timer) {
					randomTreeSpawner ();
					count = 0f;
				} else {
					treeSpawn = false;
				}
				if (mooseSpawnrate > random1000) {
					MooseSpawner ();
				}
			}
		}

			

	}

public void unPause(){
	if (paused) {
		paused = false;
	} 
}

	void randomTreeSpawner(){
		xLocation = Random.Range (-xTolerance, xTolerance);
			yLocation = Random.Range (-yTolerance, yTolerance);

		treeArr[treeCount].transform.position =  new Vector3 (xLocation, 
			                  target.position.y + yLocation + yOffset, target.position.z);
		treeCount++;
		if (treeCount == totalTrees)
			treeCount = 0;

			/*obstacleNum = Random.Range (1, 4);
			if (obstacleNum == 1) {
			} else if (obstacleNum == 2) {
			//	GameObject tree = Instantiate (tree2, new Vector3 (xLocation, 
			//		                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} else if (obstacleNum == 3) {
			//	GameObject tree = Instantiate (tree3, new Vector3 (xLocation, 
				//	                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} else  {
			//	GameObject tree = Instantiate (tree4, new Vector3 (xLocation, 
				//	                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} */
	}
	void rightTreeSpawner ()
	{
		
			xLocation = 30f;
			Debug.Log ("right tree");
			yLocation = Random.Range (-yTolerance, yTolerance);

		treeArr[treeCount].transform.position =  new Vector3 (xLocation, 
			target.position.y + yLocation + yOffset, target.position.z);
		treeCount++;
		if (treeCount == totalTrees)
			treeCount = 0;
		/*
			obstacleNum = Random.Range (1, 4);
			if (obstacleNum == 1) {
			//	GameObject tree = Instantiate (tree1Prefab, new Vector3 (xLocation, 
			//		                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} else if (obstacleNum == 2) {
			//	GameObject tree = Instantiate (tree2, new Vector3 (xLocation, 
			//		                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} else if (obstacleNum == 3) {
			//	GameObject tree = Instantiate (tree3, new Vector3 (xLocation, 
				//	                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} else  {
			//	GameObject tree = Instantiate (tree4, new Vector3 (xLocation, 
				//	                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} */ 
	}
	void leftTreeSpawner ()
	{
		
			xLocation = -30f;
			Debug.Log ("left tree");
			yLocation = Random.Range (-yTolerance, yTolerance);

		treeArr[treeCount].transform.position =  new Vector3 (xLocation, 
			target.position.y + yLocation + yOffset, target.position.z);
		treeCount++;
		if (treeCount == totalTrees)
			treeCount = 0;
		/*
			obstacleNum = Random.Range (1, 4);
			if (obstacleNum == 1) {
	//			GameObject tree = Instantiate (tree1Prefab, new Vector3 (xLocation, 
			//		                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} else if (obstacleNum == 2) {
		//		GameObject tree = Instantiate (tree2, new Vector3 (xLocation, 
		//			                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} else if (obstacleNum == 3) {
			//	GameObject tree = Instantiate (tree3, new Vector3 (xLocation, 
			//		                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} else  {
			//	GameObject tree = Instantiate (tree4, new Vector3 (xLocation, 
			//		                  target.position.y + yLocation + yOffset, target.position.z), Quaternion.identity) as GameObject;
			} */
	}

	public void MooseSpawner(){
		xLocation = Random.Range (-xTolerance, xTolerance);
		yLocation = Random.Range (-yTolerance, yTolerance);
		if (target.position.y > 500f) {
			moose.transform.position =  new Vector3 (xLocation,  target.position.y + yLocation + yOffset, target.position.z);
		}
	}
}
