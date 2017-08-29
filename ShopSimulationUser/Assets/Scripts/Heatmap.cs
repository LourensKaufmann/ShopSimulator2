using UnityEngine;
using System.Collections;
using Boo.Lang;
using LitJson;
using System.IO;
using System;

public class Heatmap : MonoBehaviour
{
    public List<Vector4> positionList = new List<Vector4>();
    public List<Vector4> propertiesList = new List<Vector4>();
    public List<float> radiusList = new List<float>();
    public List<float> intensityList = new List<float>(); 

    public Material material;   

    bool updateList = false;
    JsonData heatMapJson;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.KeypadEnter))
        //{
        //    ExportHeatMapData();
        //}        
    }

    public void InputHeatMapPositions(Vector3 position, float intensity, float radius)
    {
        positionList.Add(position);
        propertiesList.Add(new Vector4(intensity, radius, 0, 0));
    }


    //void ShowHeatMap()
    //{
    //    Vector4[] positions = positionList.GetRange(0, positionList.Count).ToArray();
    //    Vector4[] properties = propertiesList.GetRange(0, propertiesList.Count).ToArray();
        
    //    material.SetInt("_Points_Length", positions.Length);
    //    material.SetVectorArray("_Points", positions);
    //    material.SetVectorArray("_Properties", properties);
    //}

    public void ExportHeatMapData()
    {
        Vector4[] positions = positionList.GetRange(0, positionList.Count).ToArray();
        Vector4[] properties = propertiesList.GetRange(0, propertiesList.Count).ToArray();
        List<string> stringPositions = new List<string>();
        List<string> stringProperties = new List<string>();

        for (int i = 0; i < positions.Length; i++)
        {
            stringPositions.Add(positions[i].x + "," + positions[i].y + "," + positions[i].z + "," + positions[i].w);
            stringProperties.Add(properties[i].x + "," + properties[i].y + "," + properties[i].z + "," + properties[i].w);
        }
#if UNITY_EDITOR
        File.WriteAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\HeatMapData\\" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + "-HeatMapPos.txt", stringPositions.ToArray());
        File.WriteAllLines(Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName + "\\Shared Data" + "\\Data\\HeatMapData\\" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + "-HeatMapProp.txt", stringProperties.ToArray());
#else
        File.WriteAllLines(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\HeatMapData\\" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + "-HeatMapPos.txt", stringPositions.ToArray());
        File.WriteAllLines(Directory.GetParent(Application.dataPath).FullName + "\\Shared Data" + "\\Data\\HeatMapData\\" + System.DateTime.Now.ToString("MM-dd-yy_hh-mm-ss") + "-HeatMapProp.txt", stringProperties.ToArray());
#endif
    }
}