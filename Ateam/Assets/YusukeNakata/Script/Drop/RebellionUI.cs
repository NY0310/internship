using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebellionUI : MonoBehaviour {

    //あと何回で押せるようになるか
    int DisplayNumber = 3;
    public int _DisplayNumber
    {
        get { return DisplayNumber; }
        set { DisplayNumber = value; }

    }
   
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponentInChildren<Text>().text = DisplayNumber.ToString();
    }
}
