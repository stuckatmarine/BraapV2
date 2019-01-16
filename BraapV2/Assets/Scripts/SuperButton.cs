using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperButton : MonoBehaviour {

    public void ToggleSuper()
    {
        GameObject.Find("manager").GetComponent<GameManager>().SuperSledToggle();
        if (GameObject.Find("manager").GetComponent<GameManager>().iSuperSled == 0)
            GameObject.Find("superSledButton").GetComponent<Image>().color = new Color(.35f, .35f, .35f, 1.0f);
        else if (GameObject.Find("manager").GetComponent<GameManager>().iSuperSled == 1)
            GameObject.Find("superSledButton").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1.0f);
    }
}
