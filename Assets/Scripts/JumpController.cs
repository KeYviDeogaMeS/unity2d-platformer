using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float maxJumpHeight = 3f;
    [SerializeField, Range(0f, 100f)] private float minJumpHeight = 1f;
    [SerializeField, Range(0f, 1f)] private float coyoteTime = 0.2f;
    [SerializeField, Range(0f, 1f)] private float jumpBufferTime = 0.2f;

    private Rigidbody2D body;
    private CollisionCheck collisionCheck;

    private float coyoteTimeCounter;
    private float jumpBufferTimeCounter;

    private float startVerticalPos;
    private bool endJump = false;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collisionCheck = GetComponent<CollisionCheck>();
    }
    void Update()
    {
        Vector2 velocity = body.velocity;

        JumpStatsHandle();

        if (coyoteTimeCounter > 0f && jumpBufferTimeCounter > 0f)
        {
            velocity.y = Mathf.Sqrt(2 * maxJumpHeight * Mathf.Abs(Physics2D.gravity.y) * body.gravityScale);

            startVerticalPos = body.position.y;
            jumpBufferTimeCounter = 0f;
        }

        if (endJump && velocity.y > 0f)
        {
            if (body.position.y - startVerticalPos > minJumpHeight)
            {
                velocity.y *= 0.5f;

                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            coyoteTimeCounter = 0f;
        }

        body.velocity = velocity;
    }
    private void JumpStatsHandle()
    {
        if (collisionCheck.OnGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimeCounter = jumpBufferTime;
            endJump = false;
        }
        else
        {
            jumpBufferTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            endJump = true;
        }
    }
}
