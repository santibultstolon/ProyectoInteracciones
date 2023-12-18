using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int id;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    public bool isGrounded;
    public float distance;
    public bool host;
    public bool canMove;
    public Vector2 rightV = new Vector2(0.1f, 0);
        public Vector2 leftV = new Vector2(-0.1f,0);
    public bool derecha, izquierda;
    public string messages;
    public bool jump;
    public LayerMask floor;
    Animator anim;
    bool hasBall;
    GameObject ball,ballRoot;
    int valorDerecha = 1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

    }

    private void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Update()
    {
          CheckGrounded();

        Debug.Log("Se esta ejecutando");

        switch (messages)
        {
            case "derecha":
                transform.eulerAngles = (new Vector3(0, 0, 0));
                valorDerecha = 1;
                anim.SetBool("run", true);
                rb.velocity = new Vector2(rightV.x,rb.velocity.y);
                break;
            case "izquierda":
                transform.eulerAngles = (new Vector3(0, 180, 0));
                valorDerecha = -1;
                anim.SetBool("run", true);
                rb.velocity = new Vector2(leftV.x, rb.velocity.y);
                break;
            case "nothing":
                anim.SetBool("run", false);
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
        }

        
    }

    private void FixedUpdate()
    {
        if (host)
        {
            Move();
        }
        if (jump && isGrounded)
        {
            if (!hasBall)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
            }
            else if (hasBall)
            {
                ball.transform.SetParent(null);
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(3*valorDerecha, 3));
            }
           
        }
        // MovePlayer(directione);
    }

    private void Move()
    {
        Vector2 movement = moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance,floor);
        isGrounded = hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (hasBall)
            {
                Destroy(ball);
            }
            collision.gameObject.transform.position = ballRoot.transform.position;
            collision.gameObject.transform.SetParent(transform);
            hasBall = true;
            ball = collision.gameObject;


        }
    }
}
