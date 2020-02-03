using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Actor : MonoBehaviour
{
    public enum MovementDirection
    {
        LEFT,
        RIGHT,
    }
    [Header("Actor Stats")]

    public float maxHealth;
    public float currentHealth;
    public float regeration;
    public MovementDirection direction;
    public float speed;
    public float inAirSpeed;
    public float jumpForce;
    public InputAction input;

    private new Rigidbody2D rigidbody;
    private new Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;


    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        input = new InputAction();
        currentHealth = maxHealth;
    }

    public void ApplyMovement()
    {
        bool grounded = IsGrounded();
        if (grounded)
        {
            if (input.isJumping)
            {
                rigidbody.AddForce(transform.up * jumpForce);
            }
            rigidbody.velocity = new Vector2(input.horizontalInput * speed, rigidbody.velocity.y);
        }
        else
            rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x + input.horizontalInput * inAirSpeed, -speed, speed), rigidbody.velocity.y);

        //direction
        direction = (input.horizontalInput == 0) ? direction : (input.horizontalInput > 0) ? Actor.MovementDirection.RIGHT : Actor.MovementDirection.LEFT;
        spriteRenderer.flipX = direction == MovementDirection.LEFT;
    }

    public void ApplyAnimation()
    {
        if (animator.runtimeAnimatorController != null)
        {
            animator.SetBool("isMoving", !(input.horizontalInput == 0));
            animator.SetBool("isJumping", input.isJumping);
        }
    }

    public Vector3 GetCurrentVelocity() => rigidbody.velocity;

    virtual public bool IsPlayer => false;

    public float GetHealth()
    {
        return currentHealth;
    }

    virtual public void TakeDamage(float dmg)
    {
<<<<<<< HEAD
        Debug.Log("TakeDamage " + currentHealth + " -"+dmg);
        StartCoroutine(ShowDamageCoroutine());
        currentHealth -= dmg;
        if (currentHealth <= 0) Die();
=======
        Debug.Log("test22");
        if (!GetComponent<Freezable>().isFrozen)
        {
            Debug.Log("test");
            currentHealth -= dmg;
            if (currentHealth <= 0) Die();
        }
>>>>>>> eefa9e3e035ec4d5da9c56b2c2644d695ab2d376
    }

    private IEnumerator ShowDamageCoroutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    virtual public void Regenerate()
    {
        if (currentHealth != maxHealth)
            currentHealth = Mathf.Clamp( currentHealth + regeration * Time.deltaTime, 0f, maxHealth);
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died");
        //drop?
        Destroy(this.gameObject);
    }

    public bool IsGrounded()
    {
        RaycastHit2D[] result = new RaycastHit2D[2];
        collider.Raycast(Vector2.down, result, collider.bounds.extents.y + 0.05f);
        Debug.DrawRay(collider.transform.position, Vector2.down);
        //"result[0] != null" richtiger moritz move xD
        if (result != null && result[0].collider != null)
        {
            if (result[0].collider.isTrigger)
                result[0] = result[1];

            if (result[0].collider == null)
            {
                return false;
            }
            return collider.IsTouching(result[0].collider);
        }
        return false;
    }
}
