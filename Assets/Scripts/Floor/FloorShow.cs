using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorShow : MonoBehaviour
{

    //Пока нигде не используется
    [SerializeField] GameObject floor;
    bool isPlayerOnTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        isPlayerOnTrigger = true;

        if (!floor.activeSelf)
            floor.SetActive(true);
    }
}
