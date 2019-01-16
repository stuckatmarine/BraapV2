using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledColorButton2 : MonoBehaviour {

    public int color;
    public void SetColor()
    {
        GameObject.Find("manager").GetComponent<GameManager>().SetSledColor(color); 
    }
}
