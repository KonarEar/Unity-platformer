using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorMoveUp : MonoBehaviour
{
    [SerializeField] Transform platform;
    [SerializeField] Transform endPos;
    [SerializeField] private float speed;
    private bool isPlayerOnTrigger;


    private void FixedUpdate()
    {
        if (!isPlayerOnTrigger) return;
        else if (isPlayerOnTrigger)
        {
            platform.transform.position = Vector3.MoveTowards(platform.position, endPos.position, speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        platform.gameObject.SetActive(true);
        isPlayerOnTrigger = true;
    }
}
