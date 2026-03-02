using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInvisDoor : MonoBehaviour
{
    [SerializeField] GameObject door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            door.SetActive(true);
        }
    }


}
