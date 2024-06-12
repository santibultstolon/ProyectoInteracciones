/*using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerHost : MonoBehaviour
{
    public int id;
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    public bool isGrounded;
    public float distance;
    public LayerMask floor;
    Animator anim;
    [SerializeField]bool hasBall;
    public GameObject ball,ballRoot;
    int valorDerecha = 1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(rb.velocity.x + "," + rb.velocity.y);
          CheckGrounded();
        if ((Input.GetKeyDown(KeyCode.W) && isGrounded))
        {
            if (!hasBall)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
            }
            else if (hasBall)
            {
                ball.transform.SetParent(null);
                ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(200 * valorDerecha, 200));
                hasBall = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            rb.velocity = new Vector2(6, rb.velocity.y);
            valorDerecha = 1;
            anim.SetBool("run", true);

        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            rb.velocity = new Vector2(-6, rb.velocity.y);
            valorDerecha = -1;
            anim.SetBool("run", true);

        }
        else
        {
           anim.SetBool("run", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        



    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance,floor);
        isGrounded = hit.collider != null;
    }


}*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControllerHost : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    bool hasBall;
    GameObject ball;
    public GameObject ballRoot;
    int valorDerecha;


    private Rigidbody2D rb;
    private bool isGrounded;
    public float groundCheckDistance = 0.1f;
    Animator anim;
    public int points;
    public TextMeshProUGUI pointsText;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
    }

    void Update()
    {
        // Verificar si el personaje está en el suelo con un Raycast
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        pointsText.text = "x"+points.ToString();
        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        if (horizontalInput != 0)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }

        if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            valorDerecha = -1;
        }
        else if(horizontalInput > 0)
        {
            valorDerecha = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Salto
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            if (!hasBall)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (hasBall)
            {
                ball.transform.SetParent(null);
                ball.GetComponent<CircleCollider2D>().enabled = true;
                ball.AddComponent<Rigidbody2D>();
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(200 * valorDerecha, 200));
                hasBall = false;
            }
           
        }
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
            collision.gameObject.transform.SetParent(ballRoot.transform);
            hasBall = true;
            ball = collision.gameObject;
            ball.GetComponent<CircleCollider2D>().enabled = false;
        Destroy(ball.GetComponent<Rigidbody2D>());


        }
    }
}

