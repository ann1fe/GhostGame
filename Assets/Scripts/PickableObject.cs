using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickableObject : MonoBehaviour
{
    public TextMeshProUGUI collectPrompt;

    private bool isInRange; //object which we pick up
    // void Start() {
    //      collectPrompt.gameObject.SetActive(false);
    // }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInRange)
            {

                if (gameObject.CompareTag("PickUpObject")) 
                {
                    gameObject.SetActive(false);
                } else if (gameObject.CompareTag("PlaceObject")) // check from gamemanager if we have this object picked up
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                collectPrompt.gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            isInRange = true;
            collectPrompt.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            collectPrompt.gameObject.SetActive(false);
        }
    }
}