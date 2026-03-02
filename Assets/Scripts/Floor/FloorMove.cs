using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FloorMove : MonoBehaviour
{

    private Vector3 startPositionFloor;


    [Header("References")]
    [SerializeField] Transform floor;
    [SerializeField] Transform pointA;
    [SerializeField] float speed;

    private bool isPlayerOnTrigger = false;

    private void Start()
    {
        startPositionFloor = floor.position;
    }

    private void FixedUpdate()
    {
        if (isPlayerOnTrigger)
        {
            {
                var goal = new Vector3(pointA.position.x, startPositionFloor.y, startPositionFloor.z);
                floor.position = Vector3.MoveTowards(floor.position, goal, speed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnTrigger = true;
        }
    }


}
