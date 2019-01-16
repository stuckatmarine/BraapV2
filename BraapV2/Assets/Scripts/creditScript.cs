using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class creditScript : MonoBehaviour {

	public GameObject options;
	public bool optionsOpen = false;
	//public Text creditsButtonText;
	public int audioSettingIntValue = 0;
	public GameObject camera;
	public Text audioButtonText;
	public Text highScoreString;
	//public GameObject facebookPanel;
	private float highScore;
	private int sledColor;

void Start ()
{
		highScore = PlayerPrefs.GetFloat ("highScore"); //get saved highscore
}
	
public void openOptions (){

	if (!optionsOpen) {

		options.SetActive(true);
		optionsOpen = true;
        GameObject.Find("manager").GetComponent<GameManager>().GetLocalHighscore();
	} else if (optionsOpen){
		optionsOpen = false;
		options.SetActive (false);
	}
}

	public void audioSettingCaller (int num)
	{//audio on
		if (num == -4) {
			audioSettingIntValue++;
			if (audioSettingIntValue == 3) {
				audioSettingIntValue = 0;
			}
		} else if (num == 73)
        {
            ;
		} else {
			audioSettingIntValue = num;
		}

		if (audioSettingIntValue == 0) {		
			camera.GetComponent<AudioSource> ().volume = 0.8f;
			audioButtonText.text = "Audio ON";
			PlayerPrefs.SetInt ("audioSelector", 0);
		} else if (audioSettingIntValue == 1) { // bg music mute, braap still on
			camera.GetComponent<AudioSource> ().volume = 0f;
			audioButtonText.text = "Sound FX ONLY";
			PlayerPrefs.SetInt ("audioSelector", 1);
		} else if (audioSettingIntValue == 2) {
			camera.GetComponent<AudioSource> ().volume = 0f;
			PlayerPrefs.SetInt ("audioSelector", 2);
			audioButtonText.text = "All Muted";
		}
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

    public int color;
    public void SetColor()
    {
        if (highScore >= 50)
        {
            GameObject.Find("manager").GetComponent<GameManager>().SetSledColor(color);
            if (color == 1)
            {
                sledColor = 1;
                
            }
            else if (color == 2)
            {
                sledColor = 2;
                
            }
            else
            {
                sledColor = 0;
                
            }
        } 
    }
}