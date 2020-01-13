using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public float maxHealth;
    private float currentHealth;
    void Update()
    {
        input.Get();
        ApplyMovement();
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
}
