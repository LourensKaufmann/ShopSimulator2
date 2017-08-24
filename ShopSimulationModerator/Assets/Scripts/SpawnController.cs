using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    
    public List<string> spawnTypes = new List<string>();
    public List<Transform> plankTypeSelection = new List<Transform>();
 
    void Start()
    {
        Transform[] tempPlankTypeSelection = GameObject.Find("Planks").GetComponentsInChildren<Transform>();
        for (int i = 0; i < tempPlankTypeSelection.Length; i++)
        {
            if(tempPlankTypeSelection[i].tag == "Plank")
                plankTypeSelection.Add(tempPlankTypeSelection[i]);
        }
        
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Grocery")
        {
            if (col.gameObject.GetComponent<FixedJoint>() == null ) {
                for (int i = 0; i < plankTypeSelection.Count; i++)
                {
                    if (col.gameObject.GetComponent<GroceryDataHandler>().groceryName == plankTypeSelection[i].GetComponent<PlankSpawnController>().type)
                    {
                        // On the correct plank.
                        Transform[] tempPlankSpawnTypeSelection = plankTypeSelection[i].GetComponentsInChildren<Transform>();
                        List<Transform> plankSpawnTypeSelection = new List<Transform>();
                        for (int temp = 0; temp < tempPlankSpawnTypeSelection.Length; temp++)
                        {
                            if (tempPlankSpawnTypeSelection[temp].tag == "SnapZone")
                                plankSpawnTypeSelection.Add(tempPlankSpawnTypeSelection[temp]);
                        }

                        for (int j = 0; j < plankSpawnTypeSelection.Count; j++)
                        {
                            if (plankSpawnTypeSelection[j].transform.childCount < 1)
                            {
                                col.rigidbody.isKinematic = true;
                                col.rigidbody.velocity = new Vector3(0, 0, 0);
                                col.gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                                col.gameObject.transform.position = plankSpawnTypeSelection[j].gameObject.transform.position;
                                col.gameObject.transform.SetParent(plankSpawnTypeSelection[j].gameObject.transform);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
