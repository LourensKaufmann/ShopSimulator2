using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrocerySpawnHandler : MonoBehaviour {

    private void Start()
    {
        // Get the list of the grocery type selector.
        GameObject[] typeArray = transform.parent.parent.parent.parent.GetComponent<GroceryTypeSelector>().typeArray;
        string[] plank = transform.parent.parent.parent.name.Split(',');
        int plankId = int.Parse(plank[1]) * 4 - (4 - int.Parse(plank[0]));

        // Instantiate the chosen prefab and make it this child.
        if (typeArray[plankId - 1] != null && typeArray[plankId - 1].tag == "Grocery")
        {
            GameObject grocery = Instantiate(typeArray[plankId - 1], transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
            grocery.transform.localScale = transform.parent.parent.parent.parent.parent.localScale;
            grocery.transform.parent = transform;
        }
    }

}
