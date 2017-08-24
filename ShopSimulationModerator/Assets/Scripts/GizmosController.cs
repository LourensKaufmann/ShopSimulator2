using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]

public class GizmosController : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;
    private Vector3 oldPos;

	Rigidbody rb;
	Transform SnapZone;
    GameObject Zone;
    bool snapAvailable;

    [SerializeField]
    public bool isTaken = false;
    [SerializeField]
    public bool isChild = false;
    private bool getPos = true;

    void Start() {
		rb = GetComponent<Rigidbody>();
        SnapZone = null; Zone = null;
        oldPos = gameObject.transform.position;
        rb.isKinematic = true;
    }

    void OnMouseDown()
	{

		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        rb.isKinematic = false;
        transform.parent = null;
    }

    void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	void OnMouseUp(){
		rb.angularVelocity = Vector3.zero;
        if (SnapZone != null && Zone.transform.childCount < 1) {
			transform.rotation = Quaternion.Euler (0, 180, 0);
			transform.position = SnapZone.position;
            transform.position = new Vector3(SnapZone.position.x, SnapZone.position.y /*- (SnapZone.localScale.y /2)*/, SnapZone.position.z);
            transform.SetParent(Zone.transform);
            rb.isKinematic = true;
        }        
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.tag == "SnapZone" && SnapZone == null && Zone == null)
        {
            Zone = col.gameObject;
            SnapZone = col.transform;
            
        }

        
    }
    	
    void OnTriggerExit(Collider col){
		if (col.tag == "SnapZone") {
			SnapZone = null;
            Zone = null;
            //rb.isKinematic = false;
            }
	}
}