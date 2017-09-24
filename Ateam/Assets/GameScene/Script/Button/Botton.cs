using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        IsCollision = Collision.IsCollision(transform.localPosition);

       
    }

    bool IsCollision;
    public bool _IsCollision
    {
        get { return IsCollision; }
        set { IsCollision = value; }

    }

}
