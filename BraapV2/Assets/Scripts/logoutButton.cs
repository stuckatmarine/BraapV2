using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logoutButton : MonoBehaviour {
	public GameObject fbScript;
	public Button outButton;
	// Use this for initialization
	void Start () {
		fbScript = GameObject.FindGameObjectWithTag ("fbManager");
		Button btn = outButton.GetComponent<Button>();
		btn.onClick.AddListener (fbLogout);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void buttonUpdate(){
		fbScript = GameObject.FindGameObjectWithTag ("fbManager");
		fbLogout ();
	}
	void fbLogout(){
		fbScript.GetComponent<facebook> ().logout ();
	}
}
