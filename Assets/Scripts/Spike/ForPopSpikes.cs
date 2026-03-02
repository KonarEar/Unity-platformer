using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPopSpikes : MonoBehaviour
{
    [SerializeField] GameObject SpikeForFloor;
    [SerializeField] GameObject SpikeForCeiling;

    private PlayerController pc;
    private bool playerOnTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        pc = other.GetComponent<PlayerController>();
        playerOnTrigger = true;
    }

    private void Update()
    {
        if (pc == null) return;
        if (playerOnTrigger == true)
        {
            bool onFloor = pc.floorZone != null && pc.floorZone.isPlayerInside;
            bool onCeiling = pc.ceilingZone != null && pc.ceilingZone.isPlayerInside;

            if (onFloor)
            {
                SpikeForFloor.SetActive(true);
                SpikeForCeiling.SetActive(false);
            }
            if (onCeiling)
            {
                SpikeForCeiling.SetActive(true);
                SpikeForFloor.SetActive(false);
            }
        }
    }
}
