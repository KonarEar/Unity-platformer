using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeController : MonoBehaviour
{

    [SerializeField] bool for6lvl = false;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player")) {

    //        var player = other.GetComponent<PlayerController>();
    //        if (player != null)
    //        {
    //            player.PlayerDeath();
    //        }
    //    }
    //}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!for6lvl)
        {
            if (other.CompareTag("Player"))
            {

                var player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.PlayerDeath();
                }
            }
        }
        else if (for6lvl == true)
        {
            if (other.CompareTag("Player"))
            {

                var player = other.GetComponent<PlayerControllerFor6LVL>();
                if (player != null)
                {
                    player.PlayerDeath();
                }
            }
        }
    }
}
