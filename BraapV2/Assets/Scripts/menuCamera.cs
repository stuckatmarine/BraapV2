using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class menuCamera : MonoBehaviour {

public AudioClip braap;


	public Animator anim;
	private bool animStarted = false;
	private float highScore;
	public Text highScoreText;
	public int sledColor;
	public int superSledInt;
	public GameObject superSledButton;
	public GameObject red;
	public GameObject blue;
	public GameObject yel;
	public GameObject fifty;
	public GameObject twoFifty;
    public GameObject fbManager;

	void Start ()
	{
        fbManager = GameObject.Find("FacebookAndPlayFabManager");
        fbManager.GetComponent<SampleScene>().setup();
		anim = GameObject.Find("menuSledObj").GetComponent<Animator>();
		optionsLoader ();
	}

	public void startAnim(){
		if (!animStarted) {
			anim.SetBool("startClicked", true);
			animStarted = true;

		}
	}

	public void braapPlay ()
	{
		
		GetComponent<AudioSource> ().volume = 0f;
		AudioSource.PlayClipAtPoint(braap, transform.position);
	}

	public void highScoreReset(){
	PlayerPrefs.SetFloat("highScore", 0);
		optionsLoader ();
	}

	public void sledColorFunc(int num){
		if (highScore >= 50) {
			if (num == 1) {
				sledColor = 1;
				PlayerPrefs.SetInt ("sledColor", 1);
			} else if (num == 2) {
				sledColor = 2;
				PlayerPrefs.SetInt ("sledColor", 2);
			} else {
				sledColor = 0;
				PlayerPrefs.SetInt ("sledColor", 0);
			}
		}	
	}

	public void superSledToggle(){
		if (highScore >= 250) {
			if (superSledInt >= 1) {
				superSledInt = 0;
				superSledButton.GetComponent<Image> ().color = new Color (0.3f, 0.3f, 0.3f, 1f);
				PlayerPrefs.SetInt ("superSled", 0);
			} else if (superSledInt <= 0) {
				superSledInt = 1;
				PlayerPrefs.SetInt ("superSled", 1);
				superSledButton.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
			}
		}
	}

	public void optionsLoader(){
		highScore = PlayerPrefs.GetFloat ("highScore"); 
		sledColor = PlayerPrefs.GetInt ("sledColor");
		superSledInt = PlayerPrefs.GetInt ("superSled");

        highScore = (int)highScore;
		highScoreText.text = highScore.ToString ();
		if (highScore >= 50.0f) {
			fifty.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);

			red.GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
			blue.GetComponent<Image> ().color = new Color (0f, 0f, 1f, 1f);
			yel.GetComponent<Image> ().color = new Color (1f, 1f, 0f, 1f);
		}
		if (highScore >= 250.0f) {
			twoFifty.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);

		}
	}
}
