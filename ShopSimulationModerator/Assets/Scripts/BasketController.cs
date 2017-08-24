using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour {

    public DataHandler dataHandler;

    bool isColliding;
    GroceryDataHandler gData;

    Vector3 firstPos;

    private void Start()
    {
        firstPos = transform.parent.position;
    }

    private void Update()
    {
        isColliding = false;

        if (Mathf.Abs(firstPos.z - transform.parent.position.z) > 0.1)
        {
            transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, firstPos.z);
        }
        if (Mathf.Abs(firstPos.y - transform.parent.position.y) > 0.1)
        {
            transform.parent.position = new Vector3(transform.parent.position.x, firstPos.y, transform.parent.position.z);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Grocery")
        {
            gData = col.GetComponent<GroceryDataHandler>();
            if (!col.GetComponent<Rigidbody>().isKinematic && !gData.inCart)
            {
                // Grocery has been added to the shopping cart.
                if (isColliding) return;
                isColliding = true;
                gData.inCart = true;
                dataHandler.UpdateItems(gData.groceryName, 1);
                dataHandler.UpdateMatrix((int)gData.shelfPlankX, (int)gData.shelfPlankY, 1);
                col.transform.parent = transform.parent.Find("Content");
            }
        }
    }

}
