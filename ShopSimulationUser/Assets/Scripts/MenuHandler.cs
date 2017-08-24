using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

    [SerializeField]
    Camera camera;
    [SerializeField]
    private GameObject Tutorial;
    [SerializeField]
    private GameObject VRRoom;
    [SerializeField]
    private GameObject Exit;
    [SerializeField]
    private GameObject Data;
    [SerializeField]
    private GameObject ShelfSelector;
    [SerializeField]
    private GameObject Canvas;

    private bool inShelfEditor;

    private void Start()
    {
        inShelfEditor = false;
    }

    private void Update()
    {
        if (!inShelfEditor)
        {
            Canvas.SetActive(false);
        }

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "ShelfEditor")
                {
                    // Switch between Menu & Shelf editor.
                    if (!inShelfEditor)
                    {
                        inShelfEditor = true;
                        Canvas.SetActive(true);

                        Tutorial.SetActive(false);
                        VRRoom.SetActive(false);
                        Exit.SetActive(false);
                        Data.SetActive(false);
                    }
                    else
                    {
                        inShelfEditor = false;
                        Canvas.SetActive(false);

                        Tutorial.SetActive(true);
                        VRRoom.SetActive(true);
                        Exit.SetActive(true);
                        Data.SetActive(true);
                    }
                }
            }
        }
    }

}
