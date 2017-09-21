using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RebellionDrop : Drop {
    [SerializeField]
    RebellionUI RebellionUI;
    //あと何回で押せるようになるか
    int DisplayNumber = 5;
    public int _DisplayNumber
    {
        get { return DisplayNumber; }
        set { DisplayNumber = value; }

    }
    // Use this for initialization
    void Start () {
        RebellionUI = Instantiate(RebellionUI);
        RebellionUI.GetComponentInChildren<Text>().GetComponent<RectTransform>().anchoredPosition = this.transform.position;
        DisplayNumber = RebellionUI._DisplayNumber;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
