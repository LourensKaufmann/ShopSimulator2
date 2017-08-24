using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectComponentHandler : MonoBehaviour {

    List<BoxCollider> bCols = new List<BoxCollider>();
    List<MeshCollider> mCols = new List<MeshCollider>();
    [SerializeField]
    private GameObject leftController, rightController;

	// Use this for initialization
	void Start () {
        GameObject[] Groceries = GameObject.FindGameObjectsWithTag("Grocery");

        for (int i = 0; i < Groceries.Length; i++)
        {
            bCols.Add(Groceries[i].GetComponent<BoxCollider>());
            mCols.Add(Groceries[i].GetComponent<MeshCollider>());
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < bCols.Count; i++)
        {
            if (Vector3.Distance(bCols[i].transform.position, leftController.transform.position) < 1 || Vector3.Distance(bCols[i].transform.position, rightController.transform.position) < 1)
            {
                bCols[i].enabled = true;
                mCols[i].enabled = true;
            }
            else
            {
                bCols[i].enabled = false;
                mCols[i].enabled = false;
            }
        }
	}
}
