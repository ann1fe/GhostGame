using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class GhostFollow : MonoBehaviour
{
    public Transform player;


    public float minDistance = 4;
    private float lastUpdateTime = 0;
    private bool isFrozen = true;


    public void SetFrozen(bool frozen) {
        isFrozen = frozen;
    }
    // Update is called once per frame
    void Update() {
        if (Time.time - lastUpdateTime > 0.5)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            if (!isFrozen && dist > minDistance) {
                GetComponent<NavMeshAgent>().SetDestination(player.position);
            } else {
                GetComponent<NavMeshAgent>().ResetPath();
            }
        }
    }
}
