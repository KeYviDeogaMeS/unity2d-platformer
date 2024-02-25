using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float maxSpeed = 9.0f;
    public float jumpPower = 15.0f;

    Rigidbody2D rb2D = null;

    private float deltaX = 0.0f;
    private bool jump;
    private bool isGrounded = false;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        deltaX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        if (jump)
        {
            rb2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            jump = false;
        }

        Vector2 velocity = new Vector2(deltaX * maxSpeed * Time.deltaTime, rb2D.velocity.y);

        rb2D.velocity = velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
