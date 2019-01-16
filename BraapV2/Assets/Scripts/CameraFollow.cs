using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class CameraFollow : MonoBehaviour {

public Transform target;
public float cameraDistance = -10f;
public float verticalOffset = 0f;
public float horizontalOffset = 0f;
public float camSpeed =1f;
public float camMaxSpeed = .25f;
public float camIncriment = 0.000000001f;
public float audioFloatValue = 0.6f;
public float crashedOffset;

public bool crashed = false;
public Text audioText;
public bool paused = true;
private bool adShowed = false;


	// Use this for initialization
	void Start () {

		transform.position = new Vector3(horizontalOffset, target.position.y + verticalOffset, cameraDistance);

		// set the desired aspect ratio 
		float targetaspect = 16.0f / 9.0f;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

		// if scaled height is less than current height, add letterbox
		/*if (scaleheight < 1.0f)
		{
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
        */
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!paused) {
			if (crashed) {
				if ((transform.position.y - target.position.y) <= crashedOffset) {
					transform.position = new Vector3 (horizontalOffset, transform.position.y + camSpeed, cameraDistance);
				}
			} else {
				if (target.position.y >= transform.position.y) {
					transform.position = new Vector3 (horizontalOffset, target.position.y, cameraDistance);
				} else {
					transform.position = new Vector3 (horizontalOffset, transform.position.y + camSpeed, cameraDistance);
				}
				if (camSpeed <= camMaxSpeed) {
					camSpeed += camIncriment;
				}
			}
		}
	}
		
	public void unPause(){
		if (paused) {
			paused = false;
		} 
	}

IEnumerator WaitFor(float duration){
	yield return new WaitForSecondsRealtime(duration);
}

	public void ShowRewardedAd()
	{
		if (paused || crashed) {
			if (Advertisement.IsReady ("rewardedVideo") && !adShowed) {
				var options = new ShowOptions { resultCallback = HandleShowResult };
				Advertisement.Show ("rewardedVideo", options);
				adShowed = true;
			}
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			GameObject.Find ("sled").GetComponent<sled> ().gasPlusVideo ();
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}

}