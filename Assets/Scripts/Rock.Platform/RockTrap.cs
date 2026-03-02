using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrap : MonoBehaviour
{
    [SerializeField] Transform trap;
    [SerializeField] Transform rock;
    [SerializeField] Transform trapEndPosition;

    [SerializeField] GameObject rockObject;

    [SerializeField] float speed;

    private bool playerInCollision = false;


    void FixedUpdate()
    {
        if(playerInCollision == true)
        {
            rockObject.SetActive(true);
            Vector3 trapTransform = new Vector3(trapEndPosition.position.x, rock.position.y, rock.position.z);
            rock.transform.position = Vector3.MoveTowards(rock.position, trapTransform, speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollision = true;
        }
    }
   
}
