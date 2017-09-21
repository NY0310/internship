using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RestraintDropUI : MonoBehaviour {
    [SerializeField]
    GameObject buttanManager;
    BottonManager BottonManager;
    //拘束回数
    [SerializeField]
    int  MaxRestraint;
    public int _MaxRestraint
    {
        get { return MaxRestraint; }
        set { MaxRestraint = value; }

    }
    //あと何回で押せるようになるか
    int DisplayNumber;
    public int _DisplayNumber
    {
        get { return DisplayNumber; }
        set { DisplayNumber = value; }

    }
    // Use this for initialization
    void Start () {
        BottonManager = buttanManager.GetComponent<BottonManager>();

    }
	
	// Update is called once per frame
	void Update () {
        DisplayNumber = MaxRestraint - BottonManager._TouchCnt;
        this.GetComponentInChildren<Text>().text = DisplayNumber.ToString();
    }
}
