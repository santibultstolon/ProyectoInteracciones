using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
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
    public Vector2 rightV;
    public Vector2 leftV;
    public bool derecha, izquierda;
    public string messages;
    public bool jump;
    public LayerMask floor;
    Animator anim;
   public bool hasBall;
    public GameObject ball,ballRoot;
    int valorDerecha = 1;
    public int points;
    bool canJump;
    public TextMeshProUGUI pointsText;
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
        pointsText.text = "x" + points.ToString();

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
        if (jump && isGrounded&&canJump)
        {
            if (!hasBall)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
            }
            if (hasBall)
            {
                ball.transform.SetParent(null);
                ball.GetComponent<CircleCollider2D>().enabled = true;
                ball.AddComponent<Rigidbody2D>();
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(200 * valorDerecha, 200));
                hasBall = false;
                canJump = false;
            }
            

        }
        if (!jump)
        {
            canJump = true;
        }


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
            ball.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(ball.GetComponent<Rigidbody2D>());


        }
    }
}
