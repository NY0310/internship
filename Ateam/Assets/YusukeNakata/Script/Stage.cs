using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {
    //Inspectorに表示される
    [SerializeField]
    private List<ValueList> ValueListList = new List<ValueList>();

    public List<ValueList> _ValueListList
    {
        get { return ValueListList; }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

//Inspectorに複数データを表示するためのクラス
[System.SerializableAttribute]
public class ValueList
{
    public List<GameObject> List = new List<GameObject>();


    public ValueList(List<GameObject> list)
    {
        List = list;
    }
}

