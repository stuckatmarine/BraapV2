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
		GameObject.FindGameObjectWithTag ("manager").GetComponent<levelManager> ().reset ();
	}

    public void changeTo2()
    {

        GameObject.FindGameObjectWithTag("manager").GetComponent<levelManager>().ChangeToScene(2);
    }
}
