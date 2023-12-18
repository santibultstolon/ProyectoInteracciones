using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerooo : MonoBehaviour
{
    
    public float speed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject ballPrefab;
    public GameObject bolaCogida;
    public Transform throwPoint;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canJump = true;
    public bool isCarryingBall = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bolaCogida.SetActive(false);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;

        if (isGrounded)
        {
            canJump = true; 
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isCarryingBall)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false; 
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isCarryingBall)
        {
            ThrowBall();
        }

        if(isCarryingBall) 
        {
            bolaCogida.SetActive(true);
        }
    }

    void ThrowBall()
    {
        GameObject ball = Instantiate(ballPrefab, throwPoint.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x + 10f, 0f);
        isCarryingBall = false;
        bolaCogida.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isCarryingBall = true;
            Destroy(collision.gameObject);
            bolaCogida.SetActive(true);
            
        }
    }


}
