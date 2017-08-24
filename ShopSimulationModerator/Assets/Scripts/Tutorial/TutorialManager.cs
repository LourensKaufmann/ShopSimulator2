using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [HideInInspector]
    public int tutorialState;
    [SerializeField]
    private TextMesh textMesh;
    [SerializeField]
    private GameObject bubble;
    [SerializeField]
    private Camera cam;

    private void Awake()
    {
        // State 1 (Look at your hands)
        tutorialState = 1;
    }

    private void Update()
    {
        bubble.transform.LookAt(cam.transform);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorialState++;
        }
        switch (tutorialState)
        {
            case 0:
                textMesh.fontSize = 80;
                textMesh.text = "If you're ready, press the \n'continue' button on the \nceiling";
                break;
            case 1:
                // Look at your hands
                textMesh.text = "Look at your hands!";
                break;
            case 2:
                // Pull the triggers to grab items
                textMesh.fontSize = 80;
                textMesh.text = "Pull the triggers to grab \nitems";
                break;
            case 3:
                // Try to grab a pack of milk
                textMesh.text = "Try to grab a pack of milk";
                break;
            case 4:
                // Put it in your cart
                textMesh.text = "Put it in your cart";
                break;
            case 5:
                // You can also move your cart
                textMesh.text = "You can also move your \ncart";
                break;
            case 6:
                // Are you ready to begin
                textMesh.fontSize = 80;
                textMesh.text = "Are you ready to begin?\n Left trigger to stay\n Right trigger to begin";
                break;
        }
    }

}
