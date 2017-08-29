using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTimer : MonoBehaviour {

    [SerializeField]
    GameObject dHandler;
    [SerializeField]
    GameObject hMap;

    [SerializeField]
    GameObject textObject;
    TextMesh textMesh;

    [SerializeField]
    [Tooltip("In seconds")]
    float duration;
    float timeLeft;

	void Start () {
        textMesh = textObject.GetComponent<TextMesh>();
        timeLeft = duration;
	}
	
	
	void Update () {
        timeLeft -= Time.deltaTime;
        textMesh.text = ((int)timeLeft).ToString();

        if (timeLeft <= 10)
        {
            textMesh.color = Color.red;
        }

        if(timeLeft <= 0)
        {
            // End scene
            dHandler.GetComponent<DataHandler>().ExportData();
            hMap.GetComponent<Heatmap>().ExportHeatMapData();
            Application.Quit();
        }
	}
}
