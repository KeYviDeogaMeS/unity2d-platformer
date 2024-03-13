using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField, Range(0, 100f)] private float maxMoveSpeed = 4.0f;
    [SerializeField, Range(0, 100f)] private float maxJumpHeight = 4.0f;

    private Rigidbody2D body = null;
    private CollisionCheck collisionCheck;

    private float horizontalDirection;
    private bool jump;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        collisionCheck = GetComponent<CollisionCheck>();
    }
    void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && collisionCheck.OnGround)
        {
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        Vector2 velocity = body.velocity;

        if (jump)
        {
            velocity.y = Mathf.Sqrt(2 * maxJumpHeight * Mathf.Abs(Physics2D.gravity.y) * body.gravityScale);
        }

        velocity.x = horizontalDirection * maxMoveSpeed;

        body.velocity = velocity;
        jump = false;
    }
}
