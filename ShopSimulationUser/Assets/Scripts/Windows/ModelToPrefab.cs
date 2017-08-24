#if UNITY_EDITOR
using Boo.Lang;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ModelToPrefab : EditorWindow
{
    GameObject model = null;
    string name = "";

    [MenuItem("Window/Model to Prefab")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<ModelToPrefab>("Model to Prefab");
    }

    private void OnGUI()
    {
        // Input:
        GUILayout.Label("Enter the model here:", EditorStyles.boldLabel);
        model = (GameObject)EditorGUILayout.ObjectField(model, typeof(GameObject), true);
        name = EditorGUILayout.TextField("Prefab Name: ", name);
        
 
        if (GUILayout.Button("Generate"))
        {
            //Button to generate the prefab to resources folder.
            GameObject prefab = Instantiate(model);
            prefab.name = name;
            prefab.tag = "Grocery";
            prefab.layer = 2;

            // Add components.
            prefab.AddComponent<MeshFilter>();
            prefab.AddComponent<MeshRenderer>();
            prefab.AddComponent<Rigidbody>();
            prefab.AddComponent<BoxCollider>();
            prefab.AddComponent<MeshCollider>();
            prefab.AddComponent<GroceryDataHandler>();

            // Edit components.
            prefab.GetComponent<MeshCollider>().convex = true;
            prefab.GetComponent<Rigidbody>().isKinematic = true;

            // Save a prefab in resources folder.
            Object newPrefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Groceries/" + prefab.name + ".prefab");
            PrefabUtility.ReplacePrefab(prefab, newPrefab, ReplacePrefabOptions.ConnectToPrefab);

            // Remove the instantiated prefab from the scene.
            DestroyImmediate(prefab);
        }
    }
}
#endif