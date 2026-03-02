using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    //public float speed = 4f;
    //public float jumpForce = 3f;

    //private GameOver gameOver;

    //Rigidbody2D rb;
    //SpriteRenderer sr;


    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    sr = GetComponent<SpriteRenderer>();
    //    gameOver = FindObjectOfType<GameOver>();
    //}

    //private void Update()
    //{
    //    float movement = Input.GetAxis("Horizontal");
    //    transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;

    //    if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.05f)
    //    {
    //        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    //    }

    //    sr.flipX = movement > 0 ? true : false;
    //}


    //public void PlayerDeath()
    //{
    //    gameOver.OpenGameOverPanel();
    //    Destroy(gameObject);
    //}

    public float speed = 4f;
    public float jumpForce = 3f;

    private GameOver gameOver;

    Rigidbody2D rb;
    SpriteRenderer sr;

    [Header("For Gravitation")]
    [SerializeField] public bool forYGravity;
    [SerializeField] public bool useGravitationFlip;
    [SerializeField] public float baseGravitation;

    [Header("Flip Zones (level triggers)")]
    [SerializeField] public ZoneTriggerY2D floorZone;   
    [SerializeField] public ZoneTriggerY2D ceilingZone;

    private bool isPlayerInside;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gameOver = FindObjectOfType<GameOver>();

        if (useGravitationFlip == true && forYGravity == true)
        {
            rb.gravityScale = baseGravitation;
        }
    }

    private void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;

        if (!useGravitationFlip)
        {
            if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 0.05f)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
        else if (useGravitationFlip == true && forYGravity == true)
        {
            bool onFloor = floorZone.isPlayerInside;
            bool onCeiling = ceilingZone.isPlayerInside;

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (onFloor)
                {
                    // Флип к потолку
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.gravityScale = -Mathf.Abs(baseGravitation);
                    var s = transform.localScale;
                    if (s.y > 0) s.y = -s.y;
                    transform.localScale = s;
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
                else if (onCeiling)
                {
                    // Флип к полу
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.gravityScale = Mathf.Abs(baseGravitation);
                    var s = transform.localScale;
                    if (s.y < 0) s.y = -s.y;
                    transform.localScale = s;
                    rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
                }
                // Если ни в одной зоне — ничего не делаем (флип в воздухе запрещён)
            }
        
        sr.flipX = movement > 0 ? true : false;
        }
    }

    public void PlayerDeath()
    {
        gameOver.OpenGameOverPanel();
        Destroy(gameObject);
    }


}

