using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TutorialGroceryTypeSelector : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] typeArray = new GameObject[20];

   public GameObject groceryPrefab;

    private void Awake()
    {

        for (int i = 0; i < typeArray.Length; i++)
        {
            typeArray[i] = null;
        }

        typeArray[8] = groceryPrefab;
    }

}

