using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankSpawnController : MonoBehaviour {

    public string type;
    bool firstFrame;

    private void Start()
    {
        firstFrame = true;
    }

    void Update () {
        if (firstFrame)
        {
            // Check what type the groceries on this plank are.
            if (transform.GetChild(0).GetChild(0).GetChild(0).childCount > 0)
                type = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<GroceryDataHandler>().groceryName;
            else
                type = "Empty";

            firstFrame = false;
        }
    }

}
