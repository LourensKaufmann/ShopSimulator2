    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using System.IO;

public class DataHandler : MonoBehaviour {

    public float[,] matrix = new float[4,5];
    List<Item> items = new List<Item>();
    List<string> matrixList = new List<string>();
    JsonData itemJson;
    JsonData matrixJson;

    public void Update()
    {

    }

    public void UpdateMatrix(int x, int y, int amount)
    {
        matrix[x -1 , y -1] += amount;
    }

    public void UpdateItems(string itemName, int amount)
    {
        bool isInList = false;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == itemName) //Check if item is already in the list.
            {
                items[i].number += amount; //if its in the list, update quantity.
                Debug.Log("increased " + itemName + " quantity");
                isInList = true;
            }
        }

        if(isInList == false)
        {
            items.Add(new Item(itemName, amount));
            Debug.Log(itemName + " Added!");
        }
    }

    public void ExportData()
    {
        // Export matrix:
        itemJson = JsonMapper.ToJson(items);
#if UNITY_EDITOR
        File.WriteAllText(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data\\Data\\" + "Items.json", itemJson.ToString());
        System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data\\Data\\MatrixData\\" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + "-Matrix.txt");
#else
        File.WriteAllText(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data\\Data\\" + "Items.json", itemJson.ToString());
        System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data\\Data\\MatrixData\\" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + "-Matrix.txt");
#endif

        string output = "";
        for (int y = 0; y < matrix.GetLength(1); y++)
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                output += matrix[x, y].ToString();
                if(x != 3)
                output += ",";
                
            }
            streamWriter.WriteLine(output);
            matrixList.Add(output);
            output = "";
        }
        streamWriter.Close();

        itemJson = JsonMapper.ToJson(items);
        itemJson += JsonMapper.ToJson(matrixList);
#if UNITY_EDITOR
        File.WriteAllText(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\ItemsTest.json", itemJson.ToString());
#else
        File.WriteAllText(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\ItemsTest.json", itemJson.ToString());
#endif

        // Export shelfdata
#if UNITY_EDITOR
        string[] shelfSelection = File.ReadAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\shelfselection.txt");
        string[] priceSelection = File.ReadAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\priceselection.txt");
#else
        string[] shelfSelection = File.ReadAllLines(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\shelfselection.txt");
        string[] priceSelection = File.ReadAllLines(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\priceselection.txt");
#endif
#if UNITY_EDITOR
        File.WriteAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\ShelfData\\" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + "-shelfItems.txt", shelfSelection);
        File.WriteAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\ShelfData\\" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + "-shelfPrices.txt", priceSelection);
#else
        File.WriteAllText(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\ShelfData\\" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + "shelfItems", shelfSelection.ToString());
        File.WriteAllText(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\ShelfData\\" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + "shelfPrices", priceSelection.ToString());
#endif
    }
}

public class Item
{
    public string name;
    public int number;

    public Item(string newName, int amount)
    {
        name = newName;
        number = amount;
    }
}
