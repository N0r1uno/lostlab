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
        if (IsGrounded())
        {
            if (input.isJumping)
                rigidbody.AddForce(transform.up * jumpForce);
            rigidbody.velocity = new Vector2(input.horizontalInput * speed, rigidbody.velocity.y);
        } else
            rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x + input.horizontalInput * inAirSpeed, -speed, speed), rigidbody.velocity.y);

        //direction
        direction = input.direction;
        spriteRenderer.flipX = direction == MovementDirection.LEFT;
    }

    virtual public bool IsPlayer => false;

    public bool IsGrounded()
    {
        RaycastHit2D[] result = new RaycastHit2D[1];
        collider.Raycast(Vector2.down, result);
        if (result[0].collider == null)
            return false;
        return collider.IsTouching(result[0].collider);
    }

    public class InputAction
    {
        public float horizontalInput;
        public bool isJumping, isCrouching, isSprinting;
        public MovementDirection direction;

        public InputAction()
        {
            horizontalInput = 0f;
            isJumping = false;
            isCrouching = false;
            isSprinting = false;
        }

        public void Set(float horizontalInput, bool isJumping, bool isCrouching, bool isSprinting)
        {
            this.horizontalInput = horizontalInput;
            this.isJumping = isJumping;
            this.isCrouching = isCrouching;
            this.isSprinting = isSprinting;
            direction = (horizontalInput == 0) ? direction : (horizontalInput > 0) ? MovementDirection.RIGHT : MovementDirection.LEFT;
        }

        public void Get()
        {
            Set(Input.GetAxis("Horizontal"), Input.GetKeyDown(KeyCode.Space), Input.GetKey(KeyCode.LeftControl), Input.GetKey(KeyCode.LeftShift));
        }

        override //TODO
        public string ToString()
        {
            return "";
        }
    }
}
