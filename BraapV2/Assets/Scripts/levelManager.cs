using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour {

    // Scene management
    public bool skipDelay = false;
    public float changeSceneDelay = 0f;

    public void reset()
    {
		SceneManager.LoadScene(1);
	}

    public void ChangeToScene (int sceneToChangeTo)
    {
        if (skipDelay)
            SceneManager.LoadScene(sceneToChangeTo);
        else
            StartCoroutine(WaitFor(changeSceneDelay, sceneToChangeTo));
    }

    IEnumerator WaitFor(float duration, int scene)
	{
	    yield return new WaitForSecondsRealtime(duration);
	    Debug.Log("scene change to" + scene);
		SceneManager.LoadScene (scene);
	}
}
