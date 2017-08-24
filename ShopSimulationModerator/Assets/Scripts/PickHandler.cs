using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickHandler : MonoBehaviour {
    public GameObject colGO;

    int caseSwitch = 0;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "SnapZone")
        {
            colGO = col.gameObject;
        }        
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "SnapZone")
        {
            colGO = null;
        }
    }
}
