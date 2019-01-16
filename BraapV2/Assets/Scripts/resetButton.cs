using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resetButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void reset(){
		GameObject.Find("manager").GetComponent<GameManager>().ResetGame();
	}

    public void ChangeToScene(int num)
    {
        GameObject.Find("manager").GetComponent<levelManager>().ChangeToScene(0);
    }
}
