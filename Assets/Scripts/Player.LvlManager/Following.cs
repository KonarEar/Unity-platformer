using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    [SerializeField] Transform saw;
    [SerializeField] Transform player;
    [SerializeField] float speed;
    private bool isTrigger;

    private void FixedUpdate()
    {
        if (isTrigger && player != null)
        {
            var goal = new Vector3(player.position.x, player.position.y, player.position.z);
            saw.position = Vector3.MoveTowards(saw.position, goal, speed * Time.fixedDeltaTime); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        isTrigger = true;
    }

}
