using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private Interactable interactable;
    public Potion currentItem;
    public float throwStrength;
    public Transform hand;

    void Start()
    {
        Initialize();
        HealthSlider.SetValue(1f);
    }

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
        if (input.scrollWheel != 0f)
            if (input.scrollWheel > 0f)
                PotionSelector.ScrollRight();
            else
                PotionSelector.ScrollLeft();

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

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        HealthSlider.SetValue(val: currentHealth/maxHealth );
    }

    public override void Die()
    {
        //reset data
        Debug.Log("Player died");
        Vector3 checkPointPosition = CheckPointManager.GetCheckPointPos();
        transform.position = new Vector3(checkPointPosition.x, checkPointPosition.y, transform.position.z);
        currentHealth = maxHealth;
        HealthSlider.SetValue(1f);
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

    private void SetNewItem(GameObject prefab)
    {
        if (currentItem != null && !currentItem.HasBeenThrown())
            Destroy(currentItem.gameObject);

        if (prefab != null)
        {          
            currentItem = null;
            currentItem = Instantiate(prefab, hand.position, Quaternion.identity, hand).GetComponent<Potion>();
        }
        else currentItem = null;
    }

    public void UseItem()
    {

        if (currentItem == null || (currentItem != null && currentItem.type != PotionSelector.GetSelectedPotionType() && !currentItem.HasBeenThrown() ))
            if (PotionSelector.GetSelectedPotionElement().GetCount() > 0)
                SetNewItem(PotionSelector.GetSelectedPotionElement().prefab);
            else SetNewItem(null);


        if (currentItem != null && !currentItem.HasBeenThrown())        {
            if (input.isFiring)
            {
                Inventory.SubtractFromCountOfPotion(currentItem.type, 1);

                currentItem.transform.parent = null;
                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - currentItem.transform.position).normalized;
                Rigidbody2D rb = currentItem.gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 1;
                rb.AddForce(direction * throwStrength * 500);
                currentItem.Throw();   
            }
        }           
    }
}
