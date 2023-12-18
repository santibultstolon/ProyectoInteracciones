using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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
    private bool host=false;
    public bool canMove;
    public Vector2 rightV = new Vector2(0.1f, 0);
        public Vector2 leftV = new Vector2(-0.1f,0);
    public bool derecha, izquierda;
    public string messages;
    public bool jump;
    public LayerMask floor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
                Debug.Log("Derecha");
                rb.velocity = rightV;
                break;
            case "izquierda":
                Debug.Log("Izquierda");
                rb.velocity = leftV;
                break;
            case "nothing":
                Debug.Log("Res");
                rb.velocity = Vector2.zero;
                break;
        }
        if (jump&&isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
    }

    private void FixedUpdate()
    {
        if (host)
        {
            Move();
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
}
