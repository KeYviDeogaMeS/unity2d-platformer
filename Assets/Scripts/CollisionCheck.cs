using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private bool onGround;
    private bool wallSlide;

    public bool OnGround
    {
        get { return onGround; }
        set { onGround = value; }
    }
    public bool WallSlide
    {
        get { return wallSlide; }
        set { wallSlide = value; }
    }
    private void RetrieveCollision(Collision2D collision)
    {

        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y >= 0.9f)
            {
                onGround = true;
            }
            if (Mathf.Abs(contact.normal.x) >= 0.95f)
            {
                wallSlide = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RetrieveCollision(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        RetrieveCollision(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        wallSlide = false;
    }
}
