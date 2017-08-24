using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class GameManager : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;

    private void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Quaternion.AngleAxis(60, transform.right) * transform.forward, out hit))
        {
            if (hit.collider.name == "VRScene")
            {
                if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    LoadScene("VRScene");
                }
            }
            if (hit.collider.name == "HeatMapScene")
            {
                if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    LoadScene("HeatMapScene");
                }
            }
            if (hit.collider.name == "TutorialScene")
            {
                if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    LoadScene("TutorialScene");
                }
            }
            if (hit.collider.name == "Exit")
            {
                if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    LoadScene("Menu");
                }
            }
        }
    }

    void LoadScene (string sceneName)
    {
        Debug.Log("Loading: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
    
}
