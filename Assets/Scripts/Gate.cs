using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    public TextMeshProUGUI denyMessage;
    public TextMeshProUGUI allowMessage;
    public TextMeshProUGUI openUI;
    private bool isInRange;
    private bool isActive = true;
    Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            openUI.gameObject.SetActive(false);
            if(GameManager.Instance.HasMaxFollowers())
            {
                allowMessage.gameObject.SetActive(true);
                animator.SetBool("isOpen", true);
                isActive = false;
            }
            else
            {
                denyMessage.gameObject.SetActive(true);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            isInRange = true;
    
            openUI.gameObject.SetActive(true);     
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;

            openUI.gameObject.SetActive(false);
            allowMessage.gameObject.SetActive(false);
            denyMessage.gameObject.SetActive(false);
        }
    }
}
