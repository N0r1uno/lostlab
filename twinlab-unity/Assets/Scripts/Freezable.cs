using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Freezable : MonoBehaviour
{
    public Sprite freezed;
    private Sprite original;

    private new SpriteRenderer renderer;
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private Actor actor;

    public bool isFrozen;

    private bool hasAnimator = false;
    private bool hasActor = false;
    private bool hasRigidbody = false;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        actor = GetComponent<Actor>();
        hasActor = actor != null;
        hasRigidbody = rigidbody != null;
        hasAnimator = animator != null;
    }

    public void Freeze(float time)
    {
        original = renderer.sprite;
        if (hasAnimator)
            animator.enabled = false;
        if (hasActor)
            actor.enabled = false;
        if (hasRigidbody)
        {
            rigidbody.velocity = Vector2.zero;

        }
        renderer.sprite = freezed;
        isFrozen = true;
        StartCoroutine(UnfreezeCoroutine(time));
    }

    private IEnumerator UnfreezeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        Unfreeze();
    }

    public void Unfreeze()
    {
        renderer.sprite = original;
        if (hasRigidbody)
        {
            rigidbody.simulated = true;
        }
        if (hasAnimator)
            animator.enabled = true;
        if (hasActor)
            actor.enabled = true;

        isFrozen = false;
    }
}
