#if UNITY_EDITOR
using Boo.Lang;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StaticShelfSelectorWindow : EditorWindow
{

    List<string> groceryChoices;
    string[] plankNames = new string[20];
    bool[] selectedPlanks = new bool[20];

    bool firstRun = true;
    int xSize;
    int ySize;
    string groceryName;
    int choice;

    [MenuItem("Window/Static Shelf Selector")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<StaticShelfSelectorWindow>("Static Shelf Selector");
        window.minSize = new Vector2(320, 225);
    }

    void OnGUI()
    {  
        if (firstRun)
        {
            for (int i = 0; i < selectedPlanks.Length; i++)
            {
                selectedPlanks[i] = false;
                plankNames[i] = "Empty";
            }
            groceryName = "";
            choice = 0;
            firstRun = false;
        }

        GUILayout.Space(5);

        // Draw matrix.
        GUILayout.BeginHorizontal();
        GUILayout.Label("Matrix of shelf planks:", EditorStyles.boldLabel);
        if (GUILayout.Button("Empty all", GUILayout.Width(75), GUILayout.Height(20)))
        {
            for (int i = 0; i < selectedPlanks.Length; i++)
            {
                selectedPlanks[i] = false;
                plankNames[i] = "Empty";
            }
        }
        GUILayout.EndHorizontal();

        int current = 0;
        for (int i = 0; i < 5; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < 4; j++)
            {
                if (selectedPlanks[current])
                {
                    if (GUILayout.Button("", GUILayout.Width(75), GUILayout.Height(20)))
                    {
                        selectedPlanks[current] = !selectedPlanks[current];
                    }
                }
                else
                {
                    if (GUILayout.Button(plankNames[current], GUILayout.Width(75), GUILayout.Height(20)))
                    {
                        selectedPlanks[current] = !selectedPlanks[current];
                    }
                }

                current++;
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(5);

        // Button for grocery picking
        GUILayout.Label("Enter the grocery prefab name:", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Groceries");
        FileInfo[] info = dir.GetFiles("*.prefab");
        groceryChoices = new List<string>();
        for (int i = 0; i < info.Length; i++)
        {
            groceryChoices.Add(info[i].FullName.Split('\\')[info[i].FullName.Split('\\').Length - 1].Split('.')[0]);
        }
        choice = EditorGUILayout.Popup(choice, groceryChoices.ToArray(), GUILayout.Width(150));
        if (GUILayout.Button("Assign", GUILayout.Width(75), GUILayout.Height(20)))
        {
            for (int i = 0; i < selectedPlanks.Length; i++)
            {
                if (selectedPlanks[i])
                {
                    plankNames[i] = groceryChoices[choice].Split('.')[0];
                    selectedPlanks[i] = false;
                }
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        if (GUILayout.Button("Save"))
        {
            Debug.Log(choice);
            // Save the selection
            List<string> assignedPlanks = new List<string>();

            for (int i = 0; i < 20; i++)
            {
                assignedPlanks.Add(plankNames[i]);
            }

            File.WriteAllLines("Assets/Data/shelfselection.txt", assignedPlanks.ToArray());
        }

    }
}
#endif