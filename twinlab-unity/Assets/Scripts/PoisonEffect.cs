using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour
{
    private Actor actor;

    public bool isPoisoned;
    private int takesLeft;
    private float damage;

    private bool hasActor = false;
    void Start()
    {
        actor = GetComponent<Actor>();
        hasActor = actor != null;
    }

    public void Poison(float time, float dmg, int takes)
    {
        damage = dmg;
        TakeDamage(damage);
        takesLeft = takes;
        isPoisoned = true;
        StartCoroutine(UnfreezeCoroutine(time));
    }

    private IEnumerator UnfreezeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        if(takesLeft > 0)
        {
            TakeDamage(damage);
            takesLeft--;
            StartCoroutine(UnfreezeCoroutine(t));
        }
        else
        {
            Unpoision();
        }
    }

    public void Unpoision()
    {
        isPoisoned = false;
    }

    public void TakeDamage(float dmg)
    {
        if (hasActor)
        {
            actor.TakeDamage(dmg);
        }
    }
}
