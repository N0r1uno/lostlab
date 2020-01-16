using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private Interactable interactable;
    public Item currentItem;
    public float throwStrength;

    void Update()
    {
        input.Get();
        ApplyMovement();
        ApplyInteraction();
        ApplyAnimation();
        UseItem();
    }

    override
    public bool IsPlayer => true; 

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

    public void UseItem()
    {
        if (input.isFiring)
        {
            if (currentItem != null)
            {
                currentItem.transform.parent = null;
                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - currentItem.transform.position).normalized;
                Rigidbody2D rb = currentItem.gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 1;
                rb.AddForce(direction * throwStrength * 500);
                currentItem.Throw();
                currentItem = null;
            }
        }
    }
}
