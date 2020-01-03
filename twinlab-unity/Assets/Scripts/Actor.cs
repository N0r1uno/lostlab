using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Actor : MonoBehaviour
{
    public enum MovementDirection
    {
        LEFT,
        RIGHT,
    }
    [Header("Actor Stats")]
    public MovementDirection direction;
    public float speed;
    public float inAirSpeed;
    public float jumpForce;
    public InputAction input;

    private new Rigidbody2D rigidbody;
    private new Collider2D collider;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        input = new InputAction();
    }

    public void ApplyMovement()
    {
        bool grounded = IsGrounded();
        if (grounded)
        {
            if (input.isJumping)
                rigidbody.AddForce(transform.up * jumpForce);
            rigidbody.velocity = new Vector2(input.horizontalInput * speed, rigidbody.velocity.y);
        } else
            rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x + input.horizontalInput * inAirSpeed, -speed, speed), rigidbody.velocity.y);

        //direction
        direction = (input.horizontalInput == 0) ? direction : (input.horizontalInput > 0) ? Actor.MovementDirection.RIGHT : Actor.MovementDirection.LEFT;
        spriteRenderer.flipX = direction == MovementDirection.LEFT;
    }

    public Vector3 GetCurrentVelocity() => rigidbody.velocity;

    virtual public bool IsPlayer => false;

    public bool IsGrounded()
    {
        RaycastHit2D[] result = new RaycastHit2D[1];
        collider.Raycast(Vector2.down, result);
        if (result[0].collider == null)
            return false;
        return collider.IsTouching(result[0].collider);
    }
}
