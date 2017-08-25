using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GroceryTypeSelector : MonoBehaviour {

    [HideInInspector]
    public GameObject[] typeArray = new GameObject[20];
    [HideInInspector]
    public string[] priceArray = new string[20];

    private void Awake()
    {
        try
        {
#if UNITY_EDITOR
            string[] shelfSelection = File.ReadAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\shelfselection.txt");
            string[] priceSelection = File.ReadAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\priceselection.txt");
#else
            string[] shelfSelection = File.ReadAllLines(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\shelfselection.txt");
            string[] priceSelection = File.ReadAllLines(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\priceselection.txt");
#endif
            for (int i = 0; i < shelfSelection.Length; i++)
            {
                typeArray[i] = Resources.Load("Groceries/" + shelfSelection[i]) as GameObject;
                priceArray[i] = priceSelection[i];
            }
        }
        catch { Debug.Log("Error"); }
    }

}
