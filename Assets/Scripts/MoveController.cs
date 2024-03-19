using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField, Range(0, 100f)] private float maxMoveSpeed = 4.0f;
    [SerializeField, Range(0, 100f)] private float wallSlideSpeed = 2f;

    private Rigidbody2D body;
    private CollisionCheck collisionCheck;

    private float horizontalDirection;
    private float verticalDirection;
    private float defaultGravityScale;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        collisionCheck = GetComponent<CollisionCheck>();

        defaultGravityScale = body.gravityScale;
    }
    void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        verticalDirection = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Vector2 velocity = body.velocity;

        if (horizontalDirection != 0 && collisionCheck.WallSlide)
        {
                velocity.y = wallSlideSpeed * verticalDirection;
                body.gravityScale = 0;
        }
        else
        {
            body.gravityScale = defaultGravityScale;
        }

        velocity.x = horizontalDirection * maxMoveSpeed;
        body.velocity = velocity;
    }
}
