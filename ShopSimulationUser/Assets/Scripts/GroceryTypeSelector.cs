using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GroceryTypeSelector : MonoBehaviour {

    [HideInInspector]
    public GameObject[] typeArray = new GameObject[20];

    private void Awake()
    {
        try
        {
            //string[] shelfSelection = GameObject.Find("ShelfEditorHandler").GetComponent<ShelfEditorHandler>().shelfselection.ToArray();
#if UNITY_EDITOR
            string[] shelfSelection = File.ReadAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\shelfselection.txt");
#else
            string[] shelfSelection = File.ReadAllLines(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\shelfselection.txt");
#endif
            for (int i = 0; i < shelfSelection.Length; i++)
            {
                typeArray[i] = Resources.Load("Groceries/" + shelfSelection[i]) as GameObject;
            }
        }
        catch { Debug.Log("Error"); }
    }

}
