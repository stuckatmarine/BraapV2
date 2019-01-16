using UnityEngine;
using System.Collections;

public class audioButton : MonoBehaviour {

    public void CycleAudio()
    {
        GameObject.Find("manager").GetComponent<GameManager>().CycleAudio(true);
    }
}
