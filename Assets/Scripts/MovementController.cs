using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField, Range(0, 100f)] private float maxMoveSpeed = 4.0f;
    [SerializeField, Range(0, 100f)] private float maxJumpHeight = 4.0f;
    [SerializeField, Range(0, 100f)] private float minJumpHeight = 1.5f;
    [SerializeField, Range(0, 1)] private float jumpBufferingTime = 0.25f;

    private Rigidbody2D body = null;
    private CollisionCheck collisionCheck;

    private float horizontalDirection;
    private bool jump;

    private float bufferingTime;
    private float jumpStart;
    private bool endJump;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        collisionCheck = GetComponent<CollisionCheck>();
    }
    void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            bufferingTime = jumpBufferingTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            endJump = true;
        }
    }
    private void FixedUpdate()
    {
        Vector2 velocity = body.velocity;

        if (bufferingTime > 0 && collisionCheck.OnGround)
        {
            velocity.y = Mathf.Sqrt(2 * maxJumpHeight * Mathf.Abs(Physics2D.gravity.y) * body.gravityScale);
            jumpStart = transform.position.y;
        }

        if (endJump && transform.position.y - jumpStart >= minJumpHeight)
        {
            velocity.y *= 0.5f;
            endJump = false;
        }

        velocity.x = horizontalDirection * maxMoveSpeed;

        body.velocity = velocity;

        bufferingTime -= Time.deltaTime;
    }
}
