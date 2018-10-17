using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingButton : MonoBehaviour {

	public GameObject button;
	// Use this for initialization
	void Start () {
		// set the desired aspect ratio 
		float targetaspect = 16.0f / 9.0f;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

		// if scaled height is less than current height, add letterbox
	/*	if (scaleheight < 1.0f)
		{
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
        */
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine(WaitFor(8.0f));
		button.SetActive (true);
	}

	IEnumerator WaitFor(float duration)
	{
		yield return new WaitForSecondsRealtime(duration);
	}

}
