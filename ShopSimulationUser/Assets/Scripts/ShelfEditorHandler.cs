using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShelfEditorHandler : MonoBehaviour {

    List<Dropdown> dropdowns = new List<Dropdown>();
    List<string> groceryChoices;
    public List<string> shelfselection;

	public void Start () {
        DontDestroyOnLoad(gameObject);
        List<GameObject> dropdownObjects = new List<GameObject>();
 
        foreach(Transform child in GameObject.Find("ShelfChoices").transform)
        {
            dropdownObjects.Add(child.gameObject);
        }

        for (int i = 0; i < dropdownObjects.Count; i++)
        {
            // Add all dropdowns in scene to a list for later use.
            dropdowns.Add(dropdownObjects[i].GetComponent<Dropdown>());
        }

        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Groceries");
        FileInfo[] info = dir.GetFiles("*.prefab");
        groceryChoices = new List<string>();
        groceryChoices.Add("Empty");
        for (int i = 0; i < info.Length; i++)
        {
            groceryChoices.Add(info[i].FullName.Split('\\')[info[i].FullName.Split('\\').Length - 1].Split('.')[0]);
        }

        for (int i = 0; i < dropdowns.Count; i++)
        {
            // Assign the options for all the shelves.
            dropdowns[i].options.Clear();
            for (int j = 0; j < groceryChoices.Count; j++)
            {
                dropdowns[i].options.Add(new Dropdown.OptionData() { text = groceryChoices[j] });
            }
            dropdowns[i].value = 0;
            dropdowns[i].transform.Find("Label").GetComponent<Text>().text = "Empty";
        }
    }

    public void UpdateShelfSelection ()
    {
        // Update the list
        shelfselection = new List<string>();
        for (int i = 0; i < dropdowns.Count; i++)
        {
            shelfselection.Add(groceryChoices[dropdowns[i].value]);
        }
    }

    public void EmptyAll()
    {
        // Empty all selections
        for (int i = 0; i < dropdowns.Count; i++)
        {
            dropdowns[i].value = 0;
            dropdowns[i].transform.Find("Label").GetComponent<Text>().text = "Empty";
        }
    }

}
