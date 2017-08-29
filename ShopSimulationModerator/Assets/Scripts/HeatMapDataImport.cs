using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeatMapDataImport : MonoBehaviour {

    [SerializeField]
    private Dropdown heatMapDropDown;

    public HeatMapDataImport onValueChanged;

    public Material material;
    
    public TextMesh[] matrixtextRowZero, matrixtextRowOne, matrixtextRowTwo, matrixtextRowThree, matrixtextRowFour;

    //private float[,] matrix = new float[4, 5];

    // Use this for initialization
    void Start () {

        Renderer renderer = GetComponent<Renderer>();
        Material material = renderer.sharedMaterial;

        heatMapDropDown.onValueChanged.AddListener(OnValueChanged);

#if UNITY_EDITOR
        string[] heatMapData = HeatMapFilesFromFolder<Text>(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\MatrixData");
#else
        string[] heatMapData = HeatMapFilesFromFolder<Text>(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\MatrixData");
#endif
        //string[] matrixData = MatrixFilesFromFolder<Text>(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\MatrixData");

        heatMapDropDown.options.Clear();

        heatMapDropDown.options.Add(new Dropdown.OptionData() { text = "-" });

        foreach (string c in heatMapData)
        {
            //heatMapDropDown.options.Add(new Dropdown.OptionData() { text = System.IO.Path.GetFileName(c) });
            string opt = c.Split('\\')[c.Split('\\').Length - 1];
            heatMapDropDown.options.Add(new Dropdown.OptionData() { text = opt });
        }
    }

    public string[] HeatMapFilesFromFolder<T>(string folderpath) where T : UnityEngine.UI.Text
    {
        DirectoryInfo dir = new DirectoryInfo(folderpath);
        var allfiles = dir.GetFiles();
        List<string> heatMapNames = new List<string>();
        for (int i = 0; i < allfiles.Length; i++)
        {
            if (allfiles[i].FullName.Contains("-Matrix"))
            {
                heatMapNames.Add(allfiles[i].FullName);
            }
        }
        return heatMapNames.ToArray();
    }
    
    public string[] MatrixFilesFromFolder<T>(string folderpath) where T : UnityEngine.UI.Text
    {
        DirectoryInfo dir = new DirectoryInfo(folderpath);
        var allfiles = dir.GetFiles();
        List<string> matrixNames = new List<string>();
        for (int i = 0; i < allfiles.Length; i++)
        {
            if (allfiles[i].FullName.Contains("Matrix"))
            {
                matrixNames.Add(allfiles[i].FullName);
            }
        }
        return matrixNames.ToArray();
    }

    void OnValueChanged(int index)
    {
        string selection = heatMapDropDown.options[heatMapDropDown.value].text.Substring(0, 18);
#if UNITY_EDITOR
        string path = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\HeatMapData\\" + selection + "HeatMapPos.txt";
        Vector4[] positions = TextToVector4(path);
        path = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\HeatMapData\\" + selection + "HeatMapProp.txt";
        Vector4[] properties = TextToVector4(path);
#else
        string path = Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\HeatMapData\\" + selection + "HeatMapPos.txt";
        Vector4[] positions = TextToVector4(path);
        path = Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\HeatMapData\\" + selection + "HeatMapProp.txt";
        Vector4[] properties = TextToVector4(path);
#endif


        material.SetInt("_Points_Length", positions.Length);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);

        selection = heatMapDropDown.options[heatMapDropDown.value].text.Substring(0, 18);
#if UNITY_EDITOR
        path = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\MatrixData\\" + selection + "Matrix.txt";
        string[] shelfSelection = File.ReadAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\ShelfData\\" + selection + "shelfItems.txt");
        string[] priceSelection = File.ReadAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\ShelfData\\" + selection + "shelfPrices.txt");
#else
        path = Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\MatrixData\\" + selection + "Matrix.txt";
        string[] shelfSelection = File.ReadAllLines(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\ShelfData\\" + selection + "shelfItems.txt");
        string[] priceSelection = File.ReadAllLines(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\ShelfData\\" + selection + "shelfPrices.txt");
#endif
        float[,] matrix = TxtToArray(path);
        for (int i = 0; i < matrixtextRowZero.Length; i++)
        {
            matrixtextRowZero[i].text = "Type:\t\t" + shelfSelection[i] + '\n' + "Amount:\t" + matrix[0, i].ToString() + '\n' + "Price:\t\t" + priceSelection[i];
            matrixtextRowOne[i].text = "Type:\t\t" + shelfSelection[i + 4] + '\n' + "Amount:\t" + matrix[1, i].ToString() + '\n' + "Price:\t\t" + priceSelection[i + 4];
            matrixtextRowTwo[i].text = "Type:\t\t" + shelfSelection[i + 8] + '\n' + "Amount:\t" + matrix[2, i].ToString() + '\n' + "Price:\t\t" + priceSelection[i + 8];
            matrixtextRowThree[i].text = "Type:\t\t" + shelfSelection[i + 12] + '\n' + "Amount:\t" + matrix[3, i].ToString() + '\n' + "Price:\t\t" + priceSelection[i + 12];
            matrixtextRowFour[i].text = "Type:\t\t" + shelfSelection[i + 16] + '\n' + "Amount:\t" + matrix[4, i].ToString() + '\n' + "Price:\t\t" + priceSelection[i + 16];
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

        
        // Vector4 list is returned
        return heatmapPositionsVector4.ToArray();
    }

    public float[,] TxtToArray(string path)
    {
        string[] matrixLines = File.ReadAllLines(path);
        List<string[]> strings = new List<string[]>();
        float[,] matrix = new float[5, 4];

        for (int i = 0; i < matrixLines.Length; i++)
        {
            strings.Add(matrixLines[i].Split(','));
        }

        for (int i = 0; i < 5; i++)
        {
            matrix[i, 0] = float.Parse(strings[i][0]);
            matrix[i, 1] = float.Parse(strings[i][1]);
            matrix[i, 2] = float.Parse(strings[i][2]);
            matrix[i, 3] = float.Parse(strings[i][3]);
        }
        return matrix;
    }

    public void RefreshData()
    {
#if UNITY_EDITOR
        string[] heatMapData = HeatMapFilesFromFolder<Text>(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\MatrixData");
#else
        string[] heatMapData = HeatMapFilesFromFolder<Text>(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\MatrixData");
#endif

        heatMapDropDown.options.Clear();

        heatMapDropDown.options.Add(new Dropdown.OptionData() { text = "-" });

        foreach (string c in heatMapData)
        {
            string opt = c.Split('\\')[c.Split('\\').Length - 1];
            heatMapDropDown.options.Add(new Dropdown.OptionData() { text = opt });
        }

        OnValueChanged(0);
    }
}
