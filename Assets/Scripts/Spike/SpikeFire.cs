using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFire : MonoBehaviour
{
    public Animator[] spikes;   
    public float delay = 0.2f;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateSpikes());
        }
    }

    private System.Collections.IEnumerator ActivateSpikes()
    {
        foreach (Animator spike in spikes)
        {
            spike.SetTrigger("Fire");
            yield return new WaitForSeconds(delay);
        }
    }
}
