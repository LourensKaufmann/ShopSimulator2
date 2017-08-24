using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryDataHandler : MonoBehaviour {

    // Properties:
    string type;
    [HideInInspector]
    public string groceryName;
    [HideInInspector]
    public float shelfPlankX;
    [HideInInspector]
    public float shelfPlankY;
    [HideInInspector]
    public bool inCart;

    void Awake()
    {
        type = gameObject.name;
        string[] test  = type.Split('(');
        groceryName = test[0];
        inCart = false;
    }

    void Start () {

        // Calculate x,y location of original plank
        if (transform.parent.parent.parent.parent.tag == "Plank")
        {
            try
            {
                string[] plank = transform.parent.parent.parent.parent.name.Split(',');
                shelfPlankX = float.Parse(plank[0]);
                shelfPlankY = float.Parse(plank[1]);
            }
            catch
            {
                shelfPlankX = 0;
                shelfPlankY = 0;
            }
            
        }
        else
        {
            shelfPlankX = 0;
            shelfPlankY = 0;
        }
    }
	
}
