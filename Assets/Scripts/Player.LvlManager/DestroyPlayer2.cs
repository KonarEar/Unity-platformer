using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer2 : MonoBehaviour
{
    [SerializeField] GameObject player2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if(player2 != null)
        {
            Destroy(player2);
        }
    }
}
