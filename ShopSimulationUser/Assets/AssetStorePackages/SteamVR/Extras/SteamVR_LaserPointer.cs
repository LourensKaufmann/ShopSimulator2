//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public struct PointerEventArgs
{
    public uint controllerIndex;
    public uint flags;
    public float distance;
    public Transform target;
}

public delegate void PointerEventHandler(object sender, PointerEventArgs e);


public class SteamVR_LaserPointer : MonoBehaviour
{
    [SerializeField]
    Material mat;

    public bool active = true;
    public bool laserActive = false;
    public Color color;
    public float thickness = 0.002f;
    public GameObject holder;
    public GameObject pointer;
    bool isActive = false;
    public bool addRigidBody = false;
    public Transform reference;
    public event PointerEventHandler PointerIn;
    public event PointerEventHandler PointerOut;
    public GameObject continueButton, resetButton;
    public Sprite darkSprite, lightSprite;

    Transform previousContact = null;

	// Use this for initialization
	void Start ()
    {
        
	}

    public virtual void OnPointerIn(PointerEventArgs e)
    {
        if (PointerIn != null)
            PointerIn(this, e);
    }

    public virtual void OnPointerOut(PointerEventArgs e)
    {
        if (PointerOut != null)
            PointerOut(this, e);
    }


    // Update is called once per frame
	void Update ()
    {
        if (laserActive)
        {
            if (!isActive)
            {
                isActive = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            float dist = 100f;

            SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>();

            SteamVR_TrackedObject trackedObj = GetComponent<SteamVR_TrackedObject>();
            var device = SteamVR_Controller.Input((int)trackedObj.index);

            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);

            //Tutorial button checks
            if (GameObject.Find("Tutorial Manager").GetComponent<TutorialManager>().tutorialState == 9)
            {
                if (Physics.Raycast(transform.position, transform.forward, out hit) && hit.transform.gameObject == continueButton)
                {
                    Debug.Log("ContinueButton");
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = darkSprite;
                    if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        SceneManager.LoadScene("VRScene");
                    }
                }
                else if (Physics.Raycast(transform.position, transform.forward, out hit) && hit.transform.gameObject == resetButton)
                {
                    Debug.Log("ResetButton");
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = darkSprite;
                    if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        continueButton.SetActive(false);
                        resetButton.SetActive(false);
                        SceneManager.LoadScene("TutorialScene");
                    }
                }
                else
                {
                    continueButton.gameObject.GetComponent<SpriteRenderer>().sprite = lightSprite;
                    resetButton.gameObject.GetComponent<SpriteRenderer>().sprite = lightSprite;
                }
            }

            if (previousContact && previousContact != hit.transform)
            {
                PointerEventArgs args = new PointerEventArgs();
                if (controller != null)
                {
                    args.controllerIndex = controller.controllerIndex;
                }
                args.distance = 0f;
                args.flags = 0;
                args.target = previousContact;
                OnPointerOut(args);
                previousContact = null;
            }
            if (bHit && previousContact != hit.transform)
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                if (controller != null)
                {
                    argsIn.controllerIndex = controller.controllerIndex;
                }
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.transform;
                OnPointerIn(argsIn);
                previousContact = hit.transform;
            }
            if (!bHit)
            {
                previousContact = null;
            }
            if (bHit && hit.distance < 100f)
            {
                dist = hit.distance;
            }

            if (controller != null && controller.triggerPressed)
            {
                pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
            }
            else
            {
                pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            }
            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
        }        
    }

    public void FakeStart()
    {
        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.identity;

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        pointer.transform.localRotation = Quaternion.identity;
        BoxCollider collider = pointer.GetComponent<BoxCollider>();
        if (addRigidBody)
        {
            if (collider)
            {
                collider.isTrigger = true;
            }
            Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        else
        {
            if (collider)
            {
                Object.Destroy(collider);
            }
        }
        Material newMaterial = mat;
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;

        laserActive = true;
    }
}
