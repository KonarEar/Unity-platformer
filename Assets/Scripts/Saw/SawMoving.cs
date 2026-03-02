using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SawMoving : MonoBehaviour
{

    [SerializeField] Transform saw;
    [SerializeField] public float speed;

    [SerializeField] public Transform startPoint;
    [SerializeField] public Transform endPoint;

    [Header("DestroySettings")]
    [SerializeField] bool destroy = false;
    [SerializeField] float timeToDestroy;
    bool timerToDestroy;



    private void Start()
    {
        saw.position = startPoint.position;    
    }

    private void FixedUpdate()
    {
        if(destroy && timerToDestroy)
        {
            timeToDestroy -= Time.fixedDeltaTime;
        }
    }

    public void sawMoving()
    {

        if (saw != null)
        {
            var goal = new Vector3(endPoint.position.x, saw.position.y, saw.position.z);
            saw.position = Vector3.MoveTowards(saw.position, goal, speed * Time.fixedDeltaTime);
            if (destroy)
            {
                timerToDestroy = true;
                if (timeToDestroy <= 0f) Destroy(gameObject);
            }
        }
    }


}
