using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSwitch : MonoBehaviour {

    Shader outline;
    Shader standard;
    public Renderer rend;

    // Use this for initialization
    void Start ()
    {
        outline = Shader.Find("Custom/OutlineEffect");
        standard = Shader.Find("Standard");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseEnter()
    {
        rend.material.shader = outline;
        rend.material.SetColor("_OutlineColor", Color.green);
    }

    private void OnMouseDown()
    {
        rend.material.SetColor("_OutlineColor", Color.red);
    }

    private void OnMouseExit()
    {
        rend.material.shader = standard;

    }
}
