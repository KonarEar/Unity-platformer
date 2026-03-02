using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeStats : MonoBehaviour
{
    [Header("For Gravitation")]
    [SerializeField] bool forGravitation;
    [SerializeField] float baseGravitation;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;

        PlayerController pc = other.GetComponent<PlayerController>();

        if (forGravitation && pc != null)
        {
            pc.useGravitationFlip = true;
            pc.forYGravity = true;
            pc.baseGravitation = baseGravitation;
        }
    }

}
