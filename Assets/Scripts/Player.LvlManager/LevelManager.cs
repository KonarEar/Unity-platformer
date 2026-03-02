using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Animator spikesAnim;
    private bool playerInside = false;
    [SerializeField] float jumpForce;
    [SerializeField] bool forKeyDown;

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.W) && forKeyDown == true)
        {
            spikesAnim.SetTrigger("Show");
        }
        else if (playerInside && forKeyDown == false) 
        {
            spikesAnim.SetTrigger("Show");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            var player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.jumpForce = jumpForce;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            var player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.jumpForce = 3f;
            }
        }
    }

}
