#if UNITY_EDITOR
using Boo.Lang;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ShelfSelectorWindow : EditorWindow
{

    List<string> groceryChoices;
    string[] plankNames = new string[100];
    bool[] selectedPlanks = new bool[100];

    bool firstRun = true;
    int xSize;
    int ySize;
    string groceryName;
    int choice;

    [MenuItem("Window/Shelf Selector")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<ShelfSelectorWindow>("Shelf Selector");
        window.minSize = new Vector2(320, 265);
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
            xSize = 4;
            ySize = 5;
        }

        // Shelf height input.
        GUILayout.Label("Enter the shelf height:", EditorStyles.boldLabel);
        ySize = EditorGUILayout.IntField("Shelf height:", ySize);

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
        for (int i = 0; i < ySize; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < xSize; j++)
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
            // Save the selection
            List<string> assignedPlanks = new List<string>();

            for (int i = 0; i < xSize * ySize; i++)
            {
                assignedPlanks.Add(plankNames[i]);
            }

            File.WriteAllLines("Assets/Data/shelfselection.txt", assignedPlanks.ToArray());
        }

    }
}
#endif