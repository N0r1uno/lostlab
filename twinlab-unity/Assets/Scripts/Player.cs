using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public float maxHealth;
    private float currentHealth;
    private Interactable interactable;

    void Update()
    {
        input.Get();
        ApplyMovement();
        ApplyInteraction();
        ApplyAnimation();
    }

    override
    public bool IsPlayer => true; 

    public float GetHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if(currentHealth < 0.1)
        {
            Die();
        }
    }

    public void Die()
    {

    }
    public void ApplyInteraction()
    {
        if (input.isInteracting && interactable != null)
            interactable.Interact();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = collision.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.ShowMessage(true);
            Debug.Log("Entered Interactable Trigger");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>() != null)
        {
            interactable.ShowMessage(false);
            Debug.Log("Left Interactable Trigger");
            interactable = null;
        }
    }
}
