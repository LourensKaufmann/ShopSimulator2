using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (transform.GetChild(0).GetChild(0).GetChild(0) != null && transform.GetChild(0).GetChild(0).GetChild(0).childCount > 0)
                type = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<GroceryDataHandler>().groceryName;
            else
                type = "Empty";

            if (SceneManager.GetActiveScene().name == "VRScene")
            {
                int plankid = int.Parse(name.Split(',')[1]) * 4 - (4 - int.Parse(name.Split(',')[0]));
                transform.Find("Pricetag").Find("Type").GetComponent<TextMesh>().text = type;
                transform.Find("Pricetag").Find("Price").GetComponent<TextMesh>().text = transform.parent.GetComponent<GroceryTypeSelector>().priceArray[plankid - 1];
            }

            firstFrame = false;
        }
    }

}
