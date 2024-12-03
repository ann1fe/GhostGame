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

                if (gameObject.tag.StartsWith("PickUp") && GameManager.Instance.PickUpObject(gameObject.tag)) 
                {
                    gameObject.SetActive(false);
                } else if (gameObject.tag.StartsWith("Place") && GameManager.Instance.PlaceObject(gameObject.tag))
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
            var questName = gameObject.tag.Replace("PickUp", "").Replace("Place", "");
            if (GameManager.Instance.GetQuestState(questName) == QuestState.Started || 
                GameManager.Instance.GetQuestState(questName) == QuestState.ObjectPicked) 
            {
                collectPrompt.gameObject.SetActive(true);
            }
                
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