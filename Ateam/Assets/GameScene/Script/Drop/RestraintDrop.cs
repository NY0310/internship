using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestraintDrop : Drop {
    [SerializeField]
    GameObject restraintDropUI;

    RestraintDropUI RestraintDropUI;

    int MaxRestraint;
    public int _MaxRestraint
    {
        get { return MaxRestraint; }
        set { MaxRestraint = value; }
    }

    // Use this for initialization
    void Start () {
        RestraintDropUI = restraintDropUI.GetComponent<RestraintDropUI>();
        RestraintDropUI = Instantiate(RestraintDropUI);
        RestraintDropUI.transform.position = this.transform.position;
        MaxRestraint = RestraintDropUI._MaxRestraint;
    }
	
	
}
