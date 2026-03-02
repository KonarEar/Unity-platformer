using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawAnimation : MonoBehaviour
{
    [SerializeField] Transform saw;
    [SerializeField] float speed = 150f;

    private void Update()
    {
        saw.transform.Rotate(0,0, speed * Time.deltaTime);
    }


}
