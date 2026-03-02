using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrigger : MonoBehaviour
{
    [SerializeField] SawMoving saw;

    private bool isPlayerTrigger = false;

    private void FixedUpdate()
    {
        if (isPlayerTrigger) saw.sawMoving();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (!other.CompareTag("Player")) return;
        //isPlayerTrigger = true;
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            isPlayerTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        //isPlayerTrigger = false;
    }
}
