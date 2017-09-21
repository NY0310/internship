using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebellionDrop : MonoBehaviour {
    [SerializeField]
    RebellionUI RebellionUI;
    //あと何回で押せるようになるか
    int DisplayNumber;
    public int _DisplayNumber
    {
        get { return DisplayNumber; }
        set { DisplayNumber = value; }

    }
    // Use this for initialization
    void Start () {
        RebellionUI = Instantiate(RebellionUI);
        RebellionUI.transform.position = this.transform.position;
        DisplayNumber = RebellionUI._DisplayNumber;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
