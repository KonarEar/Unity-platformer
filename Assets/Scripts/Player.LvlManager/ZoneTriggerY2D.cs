using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTriggerY2D : MonoBehaviour
{
    public bool isPlayerInside;


    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.CompareTag("Player"))
            {
                isPlayerInside = true;
            }
    }

    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
