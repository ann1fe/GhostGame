using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // ?

    public int mushroomCount = 0;

    public TMP_Text mushroomText;

    public GameObject gameOverUI;
    public GameObject gameWinUI;

    public GameObject handOverMushroomsUI;
    public GameObject player;
    public GameObject playerCamera;

    void Awake()
    {
        // Set up the singleton instance
        if (Instance == null)  // ?
        {
            Instance = this;
        }
    }
    public void SetPlayerFrozen(bool frozen)
    {
   
    }

    public void NotifyDialogueEnded(string dialogueTag)
    {
        if (dialogueTag.EndsWith("Ghost")) {
            GameObject obj = GameObject.FindWithTag(dialogueTag);
            if (obj) {
                obj.GetComponent<GhostFollow>().SetFrozen(false);
            }
        }
    }
}
