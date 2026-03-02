using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float mirrorAxisX = 0f;
    [SerializeField] float mirrorAxisY = 0f;

    private int hazardLayer;

    private void Start()
    {
        hazardLayer = LayerMask.NameToLayer("Hazard");
    }

    private void Update()
    {
        if (player == null) return;
        Vector2 p = player.position;
        transform.position = new Vector2(mirrorAxisX - p.x, mirrorAxisY - p.y);
    }

    [SerializeField] PlayerControllerFor6LVL playerDeath;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(player == null) return;
        if (other.gameObject.layer == hazardLayer)
        {
            playerDeath.PlayerDeath();
            Destroy(gameObject);
        }
    }
}
