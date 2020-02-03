using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionEffect : MonoBehaviour
{
    private Actor actor;

    public bool isPoisoned;
    public int takeDamageCount;
    private int takesLeft;
    public float damage;

    private bool hasActor = false;
    void Start()
    {
        actor = GetComponent<Actor>();
        hasActor = actor != null;
    }

    public void Poison(float time)
    {
        if (hasActor)
            actor.enabled = false;
        TakeDamage(damage);
        takesLeft = takeDamageCount;
        isPoisoned = true;
        StartCoroutine(UnfreezeCoroutine(time));
    }

    private IEnumerator UnfreezeCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        if(takesLeft > 0)
        {
            TakeDamage(damage);
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
