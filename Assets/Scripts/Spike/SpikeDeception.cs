using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDeception : MonoBehaviour
{

    public Animator spikeAnim;
    [SerializeField] GameObject spike;

    bool showOnce = false;
    [SerializeField] bool spikesMove = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (showOnce) return;

        showOnce = true;

        if (!spike.activeSelf)
            spike.SetActive(true);

        if (!spikesMove)
        {
            spikeAnim.SetTrigger("Show");
        }
        else if (spikesMove)
        {
            {
                spikeAnim.SetTrigger("Move");
            }
        }

    }
}
