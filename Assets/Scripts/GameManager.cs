using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using Unity.Properties;
using Unity.VisualScripting;
public enum QuestState{
    NotStarted,
    Started,
    ObjectPicked,
    ObjectPlaced,
    Ended
};
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // ?

    public GameObject player;
    public GameObject playerCamera;
    public int followCount;
    public GameObject instructions;

    Dictionary<string, QuestState> questState = new Dictionary<string, QuestState>(); 

    void Awake()
    {
        // Set up the singleton instance
        if (Instance == null)  // ?
        {
            Instance = this;
        }
    }
    void Start() {
        Cursor.lockState = CursorLockMode.None;
    }
    public void DisableInstructions() {
        Cursor.lockState = CursorLockMode.Locked;
        instructions.SetActive(false);
    }
    public bool HasMaxFollowers()
    {
        return followCount == 5;
    }

    public QuestState GetQuestState(string questName)
    {
        if (!questState.ContainsKey(questName))
        {
            questState[questName] = QuestState.NotStarted; // Default value if quest doesn't exist
        }
        return questState[questName]; // Return the current state of the quest
    }
    // public void SetQuestState(string questName, QuestState state)
    // {
    //     questState[questName] = state; // Update the existing quest's state
    // }

    public void NotifyDialogueEnded(string dialogueTag)
    {
        if (dialogueTag.EndsWith("Ghost")) {
            var questName = dialogueTag.Replace("Ghost", "");
            if (GetQuestState(questName) == QuestState.NotStarted) 
            {
                questState[questName] = QuestState.Started;
            } 
            else if (((questName == "Teddy" || questName == "Food") && GetQuestState(questName) == QuestState.ObjectPicked) || 
                GetQuestState(questName) == QuestState.ObjectPlaced) 
            {
                questState[questName] = QuestState.Ended;
                GameObject[] objects = GameObject.FindGameObjectsWithTag(dialogueTag);
                foreach (var obj in objects)
                {
                    obj.GetComponent<GhostFollow>().SetFrozen(false);
                    followCount ++;
                }
            }
        }
    }

    public bool PickUpObject(string objectTag)
    {
        var questName = objectTag.Replace("PickUp", "");
        if (GetQuestState(questName) == QuestState.Started)
        {
            questState[questName] = QuestState.ObjectPicked;
            return true;
        }
        return false;
    }

    public bool PlaceObject(string objectTag)
    {
        var questName = objectTag.Replace("Place", "");
        if (GetQuestState(questName) == QuestState.ObjectPicked)
        {
            questState[questName] = QuestState.ObjectPlaced;
            return true;
        }
        return false;
    }

    public int GetDialogueIndex(string objectTag)
    {
        if (objectTag == "CandleGhost") {
            if (GetQuestState("Candle") == QuestState.ObjectPlaced) {
                return 1;
            }
        }
        else if (objectTag == "FlowerGhost") 
        {
            if (GetQuestState("Flower") == QuestState.ObjectPlaced)
            {
                return 1;
            }
        }
             else if (objectTag == "FoodGhost") 
        {
            if (GetQuestState("Food") == QuestState.ObjectPicked)
            {
                return 1;
            }
        }
        else if (objectTag == "TeddyGhost") 
        {
            if (GetQuestState("Teddy") == QuestState.ObjectPicked)
            {
                return 1;
            }
        }
        return 0;
    }
}
