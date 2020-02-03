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
    public bool invertedControls;

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
        if (invertedControls)
            input.Invert();

        if (IsGrounded())
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
        //Debug.Log("TakeDamage " + currentHealth + " -"+dmg);
        if (!GetComponent<Freezable>().isFrozen)
        {
            StartCoroutine(ShowDamageCoroutine());
            currentHealth -= dmg;
            if (currentHealth <= 0) Die();
        }
    }

    private IEnumerator ShowDamageCoroutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    public void InvertControls(float time)
    {
        invertedControls = true;
        StartCoroutine(ResetInvertion(time));
    }

    private IEnumerator ResetInvertion(float time)
    {
        yield return new WaitForSeconds(time);
        invertedControls = false;
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
         Vector2 startPos = new Vector2(collider.bounds.center.x - collider.bounds.extents.x, collider.bounds.center.y);
         RaycastHit2D resL = Physics2D.Raycast(startPos, Vector2.down, collider.bounds.extents.y + 0.2f, LayerMask.GetMask("jumpable"));
        startPos = new Vector2(collider.bounds.center.x + collider.bounds.extents.x, collider.bounds.center.y);
         RaycastHit2D resR = Physics2D.Raycast(startPos, Vector2.down, collider.bounds.extents.y + 0.2f, LayerMask.GetMask("jumpable"));


         if (resR.collider != null || resL.collider != null)
         {
             return true;
         }
         return false;
     }
}
