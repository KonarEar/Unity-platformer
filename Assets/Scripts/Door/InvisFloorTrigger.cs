using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisFloorTrigger : MonoBehaviour
{
    public Animator spikeAnim;
    [SerializeField] GameObject spike;
    private bool playerInside = false;

    private void Update()
    {
        if (playerInside)
        {
            spike.gameObject.SetActive(true);
            spikeAnim.SetTrigger("Show");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }
}
