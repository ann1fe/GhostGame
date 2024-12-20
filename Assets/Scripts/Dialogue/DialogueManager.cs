using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
 
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
 
    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
 
    private Queue<DialogueLine> lines;
    private string dialogueTag;
    
    public float typingSpeed = 0.2f;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
 
        lines = new Queue<DialogueLine>();
    }
 
    public void StartDialogue(Dialogue dialogue, string tag)
    {
        lines.Clear();
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }
        dialogueTag = tag;
        DisplayNextDialogueLine();

        //Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
 
    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
 
        DialogueLine currentLine = lines.Dequeue();
 
        characterName.text = currentLine.character.name;
 
        StopAllCoroutines();
 
        StartCoroutine(TypeSentence(currentLine));
    }
 
    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
 
    void EndDialogue()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.NotifyDialogueEnded(dialogueTag);
    }
}