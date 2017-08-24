using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveHeatMapHeadSetController : MonoBehaviour {

    Vector3 direction;
    RaycastHit heatMapRayCast;

    public Heatmap heatMap;

    //private float nextActionTime = 1.0f;
    public float period = 0.0f;

    // Use this for initialization
    void Start () {
       // direction = transform.TransformDirection(Vector3.forward);
        Ray direction = new Ray(transform.position, transform.forward);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.forward);

        if (period > 0.1f)
        {
            if (Physics.Raycast(transform.position, transform.forward, out heatMapRayCast) && heatMapRayCast.transform.gameObject.tag == "HeatMapPlane")
            {
                float distance = Vector3.Distance(transform.position, heatMapRayCast.transform.position);
                float radius = distance * 0.08333f;
                float intensity = (distance * -0.4167f) + 1;

                heatMap.InputHeatMapPositions(heatMapRayCast.point, radius, intensity);
            }
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;

        
    }
}