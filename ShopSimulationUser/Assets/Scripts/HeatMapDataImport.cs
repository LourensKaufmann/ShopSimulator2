using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeatMapDataImport : MonoBehaviour {

    [SerializeField]
    private Dropdown heatMapDropDown, matrixDropDown;

    public HeatMapDataImport onValueChanged;

    public Material material;
    
    public TextMesh[] matrixtextRowZero, matrixtextRowOne, matrixtextRowTwo, matrixtextRowThree, matrixtextRowFour;

    //private float[,] matrix = new float[4, 5];

    // Use this for initialization
    void Start () {

        Renderer renderer = GetComponent<Renderer>();
        Material material = renderer.sharedMaterial;

        heatMapDropDown.onValueChanged.AddListener(OnHeatMapValueChanged);
        matrixDropDown.onValueChanged.AddListener(OnMatrixValueChanged);

        string[] heatMapData = HeatMapFilesFromFolder<Text>(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\HeatMapData");
        string[] matrixData = MatrixFilesFromFolder<Text>(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\MatrixData");

        heatMapDropDown.options.Clear();
        matrixDropDown.options.Clear();

        heatMapDropDown.options.Add(new Dropdown.OptionData() { text = "-" });
        matrixDropDown.options.Add(new Dropdown.OptionData() { text = "-" });

        foreach (string c in heatMapData)
        {
            heatMapDropDown.options.Add(new Dropdown.OptionData() { text = System.IO.Path.GetFileName(c) });
        }
        foreach (string c in matrixData)
        {
            matrixDropDown.options.Add(new Dropdown.OptionData() { text = System.IO.Path.GetFileName(c) });
        }

        Debug.Log(heatMapData.Length);
    }

    public string[] HeatMapFilesFromFolder<T>(string folderpath) where T : UnityEngine.UI.Text
    {
        var allfiles = Resources.LoadAll(folderpath);
        List<string> heatMapNames = new List<string>();
        for (int i = 0; i < allfiles.Length; i++)
        {
            if (allfiles[i].name.Contains("Pos"))
            {
                heatMapNames.Add(allfiles[i].name);
            }
        }
        return heatMapNames.ToArray();
    }
    
    public string[] MatrixFilesFromFolder<T>(string folderpath) where T : UnityEngine.UI.Text
    {
        var allfiles = Resources.LoadAll(folderpath);
        List<string> matrixNames = new List<string>();
        for (int i = 0; i < allfiles.Length; i++)
        {
            if (allfiles[i].name.Contains("Matrix"))
            {
                matrixNames.Add(allfiles[i].name);
            }
        }
        return matrixNames.ToArray();
    }

    void OnHeatMapValueChanged(int index)
    {
        Debug.Log("OnHeatMapValueChanged");
        string selection = heatMapDropDown.options[heatMapDropDown.value].text.Substring(0, 25);
        string path = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\HeatMapData" + selection + "Pos.txt";
        Vector4[] positions = TextToVector4(path);
        path = Application.dataPath + Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\HeatMapData" + selection + "Prop.txt";
        Vector4[] properties = TextToVector4(path);

        material.SetInt("_Points_Length", positions.Length);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);
    }

    void OnMatrixValueChanged(int index)
    {
        Debug.Log("OnMatrixValueChanged");
        string selection = matrixDropDown.options[matrixDropDown.value].text.Substring(0, 18);
        string path = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\MatrixData" + selection + "Matrix.txt";
        
        float[,] matrix = TxtToArray(path);

        for (int i = 0; i < matrixtextRowZero.Length; i++)
        {
            Debug.Log(matrix[4,0]);
            matrixtextRowZero[i].text = matrix[0, i].ToString();
            matrixtextRowOne[i].text = matrix[1, i].ToString();
            matrixtextRowTwo[i].text = matrix[2, i].ToString();
            matrixtextRowThree[i].text = matrix[3, i].ToString();
            matrixtextRowFour[i].text = matrix[4, i].ToString();
        }
    }

    public Vector4[] TextToVector4(string path)
    {
        // This method takes a path to the saved heatmap data and converts it to a Vector4 array which it returns together with an array of properties. TextToVector4[0] = Positions, TextToVector4[1] = Properties.
        string[] heatmapPositionLines;
        List<string[]> heatmapPositionArrayLines = new List<string[]>();
        List<Vector4> heatmapPositionsVector4 = new List<Vector4>();
        List<Vector4> heatmapPropertiesVector4 = new List<Vector4>();

        // Read the text file as an array where every line is a string.
        heatmapPositionLines = File.ReadAllLines(path);

        for (int i = 0; i < heatmapPositionLines.Length; i++)
        {
            // Splitting the string array which gives a new array every line, each one of these is added to a list.
            heatmapPositionArrayLines.Add(heatmapPositionLines[i].Split(','));
        }

        for (int i = 0; i < heatmapPositionArrayLines.Count; i++)
        {
            // Every string array in the list is converted to a Vector4 and added to a list.
            heatmapPositionsVector4.Add(new Vector4(float.Parse(heatmapPositionArrayLines[i][0]), float.Parse(heatmapPositionArrayLines[i][1]), float.Parse(heatmapPositionArrayLines[i][2]), float.Parse(heatmapPositionArrayLines[i][3])));
        }

        
        // Two Vector4 lists are returned
        return heatmapPositionsVector4.ToArray();
    }

    public float[,] TxtToArray(string path)
    {
        Debug.Log("TxtToArray");
        string[] matrixLines = File.ReadAllLines(path);
        List<string[]> strings = new List<string[]>();
        float[,] matrix = new float[5, 4];

        for (int i = 0; i < matrixLines.Length; i++)
        {
            strings.Add(matrixLines[i].Split(','));
        }

        for (int i = 0; i < 5; i++)
        {
            //int count = 0;
            matrix[i, 0] = float.Parse(strings[i][0]);
            matrix[i, 1] = float.Parse(strings[i][1]);
            matrix[i, 2] = float.Parse(strings[i][2]);
            matrix[i, 3] = float.Parse(strings[i][3]);
            //matrix[i, 4] = float.Parse(strings[i][4]);
        }
        return matrix;
    }
}
