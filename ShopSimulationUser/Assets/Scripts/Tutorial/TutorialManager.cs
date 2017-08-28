using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [HideInInspector]
    public int tutorialState;
    [SerializeField]
    private TextMesh textMesh;
    [SerializeField]
    private Camera cam;

    TutorialGrocerySpawnHandler spawnhandler = new TutorialGrocerySpawnHandler();

    float spaceTimer = 5f;

    Color colorStart;
    Color colorEnd;
    [SerializeField]
    private GameObject shelf;
    [SerializeField]
    private GameObject cart;
    private float duration = 5;
    [SerializeField]
    private GameObject continueButton, replayButton;
    [SerializeField]
    private GameObject controller;
    [SerializeField]
    private GameObject arrow;

    private bool isLookingAtShelf;
    public bool hasPressedTrigger = false; 

    //private Animation anim;

    private void Awake()
    {
        // State 1 (Look at your hands)
        tutorialState = 1;
    }

    void start()
    {
        //anim = shelf.GetComponent<Animation>();
        //colorStart = shelf.GetComponent<Renderer>().material.color;
        //colorEnd = new Color(colorStart.r, colorStart.g, colorStart.b, 0.0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorialState++;
        }

        if (tutorialState == 8)
        {
            if (shelf.GetComponent<Renderer>().isVisible) //check if shelf is in screen
            {
                isLookingAtShelf = true;
            }
        }

        if (arrow != null)
        {
            arrow.gameObject.transform.LookAt(shelf.transform);
        }

        switch (tutorialState)
        {
            case 0:
                textMesh.fontSize = 80;
                textMesh.text = "";
                break;
            case 1:
                // Look at your hands
                textMesh.text = "Kijk naar je handen!";
                break;
            case 2:
                // Pull the triggers to grab items
                textMesh.text = "Druk de triggers op de achterkant van je \ncontrollers en kijk wat er \ngebeurd met je handen!";                
                break;
                
            case 3:
                // Try to grab a pack of milk
                textMesh.text = "";
                bool hasPressedSpace = true;

                if (hasPressedSpace)
                {
                    spaceTimer -= Time.deltaTime;
                    Debug.Log(spaceTimer);
                }
                if (spaceTimer <= 0f && isLookingAtShelf == true)
                {
                    //FadeOut();
                    shelf.GetComponent<Animation>().Play("FadeIn");
                    hasPressedSpace = false;
                    tutorialState = 4;
                }
                break;
            case 4:
                textMesh.fontSize = 80;
                textMesh.text = "Probeer een pak melk op te pakken";
                break;
            case 5:
                // Put it in your cart
                textMesh.text = "Leg het pak nu in je winkelwagen";
                cart.GetComponent<Animation>().Play("CartFadeIn");
                break;
            case 6:
                // You can also move your cart
                textMesh.text = "Door op het handvat van de winkelwagen \nte drukken kun je je winkelwagen bewegen";
                break;
            case 7:
                // Are you ready to begin
                textMesh.text = "Richt met je controller naar \neen knop en druk op de trigger \nom een keuze te maken";
                controller.GetComponent<SteamVR_LaserPointer>().active = true;
                cart.SetActive(false);
                shelf.SetActive(false);
                replayButton.SetActive(true);
                continueButton.SetActive(true);
                //Buttons
                //textMesh.fontSize = 80;
                //textMesh.text = "Are you ready to begin?\n Left trigger to stay\n Right trigger to begin";
                break;
            case 8:
                if (!isLookingAtShelf)
                {
                    arrow.SetActive(true);
                }else
                {
                    arrow.SetActive(false);
                    tutorialState = 3;
                }
                break;
        }
    }

    //void FadeOut()
    //{
    //    for (float i = 0; i < duration; i += Time.deltaTime)
    //    {
    //        shelf.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, i / duration);
    //    }
    //}

}
