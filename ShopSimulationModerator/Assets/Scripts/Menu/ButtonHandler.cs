using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    [SerializeField]
    Camera camera;

    SteamVR_TrackedObject trackedObj;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "VRScene")
                {
                    SceneManager.LoadScene("VRScene");
                }
                if (hit.collider.name == "TutorialScene")
                {
                    SceneManager.LoadScene("TutorialScene");
                }
                if (hit.collider.name == "Data")
                {
                    SceneManager.LoadScene("HeatMapScene");
                }
            }

        }
    }
}
