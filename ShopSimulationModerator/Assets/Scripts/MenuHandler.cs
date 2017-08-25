using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    [SerializeField]
    Camera camera;

    [SerializeField]
    private GameObject canvas1;
    [SerializeField]
    private GameObject canvas2;
    [SerializeField]
    private GameObject matrix;
    [SerializeField]
    private GameObject shelf;

    private void Start()
    {
        canvas2.SetActive(false);   
    }

    public void SwitchScreen()
    {
        if (canvas1.active)
        {
            // Data is active so switch to Editor.
            canvas1.SetActive(false);
            matrix.SetActive(false);
            shelf.SetActive(false);
            canvas2.SetActive(true);
        }

        else if (canvas2.active)
        {
            // Editor is active so switch to Data.
            canvas1.SetActive(true);
            matrix.SetActive(true);
            shelf.SetActive(true);
            canvas2.SetActive(false);
        }
        
    }

}
