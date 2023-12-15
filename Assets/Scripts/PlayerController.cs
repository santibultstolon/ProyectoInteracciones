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
    private bool isGrounded;
    private bool host=false;
    public bool canMove;
    public Vector2 directione;

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

    }

    private void FixedUpdate()
    {
        if (host)
        {
            Move();
        }
        if (directione.x == 0)
        {
            canMove = false;
        }
        if (canMove)
        {
            MovePlayer(directione);
        }


    }
    public void MovePlayer(Vector2 direction)
    {
        transform.Translate(direction);
    }

    private void Move()
    {
        Vector2 movement = moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        isGrounded = hit.collider != null;
    }
}
