using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private Interactable interactable;
    public Potion currentItem;
    public float throwStrength;
    public Transform hand;

    void Update()
    {
        input.Get();
        ApplyMovement();
        ApplyInteraction();
        ApplyAnimation();
        UseItem();
        Regenerate();
    }

    override
    public bool IsPlayer => true; 

    public void ApplyInteraction()
    {
        int alphakey = input.GetAlphaKey();
        if (alphakey >= 1 && alphakey <= 5)
        {
            PotionSelector.Set(alphakey - 1);
        } else
        {
            if (input.scrollWheel != 0f)
                if (input.scrollWheel > 0f)
                    PotionSelector.ScrollLeft();
                else
                    PotionSelector.ScrollRight();
        }

        if (input.isInteracting && interactable != null)
            interactable.Interact();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = collision.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.ShowMessage(true);
        }
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        HealthSlider.SetValue(currentHealth/maxHealth);
    }

    public override void Regenerate()
    {
        base.Regenerate();
        HealthSlider.SetValue(currentHealth / maxHealth);
    }

    public override void Die()
    {
        //reset data
        Freezable f = GetComponent<Freezable>();
        if (f != null) f.Unfreeze();

        SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
        foreach (SpawnPoint point in spawnPoints)
            point.ResetSpawn();

        Vector3 checkPointPosition = CheckPointManager.GetCheckPointPos();
        transform.position = new Vector3(checkPointPosition.x, checkPointPosition.y, transform.position.z);

        foreach (SpawnPoint point in spawnPoints)
            point.SpawnAll();

        currentHealth = maxHealth;
        HealthSlider.SetValue(1f);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>() != null)
        {
            if (interactable != null)
            {
                interactable.ShowMessage(false);
                interactable = null;
            }
        }
    }

    public void UseItem()
    {
        PotionSelector.PotionElement selected = PotionSelector.GetSelectedPotionElement();

        //currentItem ist momentan leer, aber neues, verfuegbares potion ist ausgewaehlt
        if (currentItem == null && selected.GetCount() > 0)
            currentItem = Instantiate(selected.prefab, hand.position, Quaternion.identity, hand).GetComponent<Potion>();

        //currentItem ist vorhanden
        if (currentItem != null)
        {
            //currentItem wurde noch nicht geworfen und es wurde ein neues potion gewaehlt
            if (!currentItem.HasBeenThrown() && PotionSelector.GetSelectedPotionType() != currentItem.type)
            {
                //momentan ausgewaehltes zerstoeren
                Destroy(currentItem.gameObject);
                currentItem = null;
                //falls neues ausgewaehltes verfuegbar, erstellen
                if (selected.GetCount() > 0)
                    currentItem = Instantiate(selected.prefab, hand.position, Quaternion.identity, hand).GetComponent<Potion>();
            }

            //falls mausklick, currentItem vorhanden und momentanes currentItem noch nicht geworfen...
            if (input.isFiring && currentItem != null && !currentItem.HasBeenThrown())
            {
                ThrowPotion(); //werfen
                StartCoroutine(RespawnPotionCoroutine(currentItem)); //respawnroutine starten
            }
        }
    }

    private void ThrowPotion()
    {
        Inventory.SubtractFromCountOfPotion(currentItem.type, 1);
        currentItem.transform.parent = null;
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - currentItem.transform.position).normalized;
        Rigidbody2D rb = currentItem.gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.AddForce(direction * throwStrength * 500);
        currentItem.Throw();        
    }

    IEnumerator RespawnPotionCoroutine(Potion thrownPotion)
    {
        yield return new WaitForSeconds(0.2f); //cooldown
        PotionSelector.PotionElement selected = PotionSelector.GetSelectedPotionElement();
        //wenn currentItem==null ist das potion wahrscheinlich schon geplatzt, ersetzen in UseItem()
        //wenn currentItem vorhanden
        if (currentItem != null)
        {
            //geworfenen Potion ist noch im flug
            if (currentItem == thrownPotion)
            {
                if (selected.GetCount() > 0)
                    currentItem = Instantiate(selected.prefab, hand.position, Quaternion.identity, hand).GetComponent<Potion>();
            } 
        }    
    }
}
