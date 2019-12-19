using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
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
    public float jumpForce;
    public InputAction input;

    private new Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        input = new InputAction();
    }

    public void ApplyMovement()
    {
        //rigidbody.velocity.Set(input.horizontalInput * speed, jumpForce);
        rigidbody.velocity = new Vector2(input.horizontalInput * speed, input.isJumping?jumpForce:0);
        direction = input.direction;
    }

    virtual public bool IsPlayer => false;

    public class InputAction
    {
        public float horizontalInput;
        public bool isJumping, isCrouching;
        public MovementDirection direction;

        public InputAction()
        {
            this.horizontalInput = 0f;
            this.isJumping = false;
            this.isCrouching = false;
        }

        public void Set(float horizontalInput, bool isJumping, bool isCroutching)
        {
            this.horizontalInput = horizontalInput;
            this.isJumping = isJumping;
            this.isCrouching = isCroutching;
            direction = (horizontalInput == 0) ? direction : (horizontalInput > 0) ? MovementDirection.RIGHT : MovementDirection.LEFT;
        }

        public void Get()
        {
            Set(Input.GetAxis("Horizontal"), Input.GetKeyDown(KeyCode.Space), Input.GetKey(KeyCode.LeftControl));
        }

        override
        public string ToString()
        {
            return "";
        }
    }
}
