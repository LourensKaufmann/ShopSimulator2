using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGrocerySpawnHandler : MonoBehaviour
{
    TutorialManager tutorialManager;
    bool isSpawned;

    void Start()
    {
        tutorialManager = GameObject.Find("Tutorial Manager").GetComponent<TutorialManager>();
    }

    public void Spawn()
    {
        // Get the list of the grocery type selector.
        GameObject[] typeArray = transform.parent.parent.parent.parent.GetComponent<TutorialGroceryTypeSelector>().typeArray;
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

    void Update()
    {
        if (tutorialManager.tutorialState == 5 && isSpawned == false)
        {
            Invoke("Spawn", 3);
            isSpawned = true;
        }
    }

}
