using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePotion : Potion
{
    [Header("Freeze Effect")]
    public float maxFreezeTime;
    public float freezeDmg;

    [Header("Fire Effect")]
    public float fireDmg;

    [Header("Poison Effect")]
    public float poisonDmg;
    public int poisonTurns;
    public float poisonTime;

    [Header("Gravity Effect")]
    public GameObject gravityFieldPrefab;

    [Header("Push Effect")]
    public float pushForce;

    [Header("Invert Effect")]

    [Header("Light Effect")]
    public GameObject lightPrefab;





    public new void Start()
    {
        base.Start();
        type = Type.purple;
    }

    public override void Effect()
    {
        int random = Random.Range(1, 101);
        if(random > 0 && 20 > random)
        {
            Debug.Log("freeze");
            List<Actor> actors = GetAllHitActors();
            foreach (Actor actor in actors)
            {
                float distanceMultiplicator = GetDistanceMultiplier(actor);
                actor.TakeDamage(distanceMultiplicator * freezeDmg);

                Freezable f = actor.GetComponent<Freezable>();
                if (f != null)
                    f.Freeze(distanceMultiplicator * maxFreezeTime);
            }
        }
        else if (random > 20 && 40 > random)
        {
            Debug.Log("fire");
            List<Actor> actors = GetAllHitActors();
            foreach (Actor actor in actors)
                actor.TakeDamage(GetDistanceMultiplier(actor) * fireDmg);
        }
        else if (random > 40 && 60 > random)
        {
            Debug.Log("poison");
            List<Actor> actors = GetAllHitActors();
            foreach (Actor actor in actors)
            {
                actor.GetComponent<PoisonEffect>().Poison(poisonTime, poisonDmg, poisonTurns);
            }
        }
        else if(random > 60 && 80 > random)
        {
            Debug.Log("electro");
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, range);
            for (int i = 0; i < collisions.Length; i++)
            {
                Cable cable = collisions[i].GetComponent<Cable>();
                if (cable != null)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, collisions[i].transform.position - transform.position);
                    if (hit.collider.gameObject.Equals(collisions[i].gameObject))
                    {
                        cable.GiveSignal();
                    }
                }
            }
        }
        else if(random > 80 && 85 > random)
        {
            Debug.Log("gravity");
            Instantiate(gravityFieldPrefab, transform.position, Quaternion.identity);
        }
        else if(random > 85 && 90 > random)
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, range);
            for (int i = 0; i < collisions.Length; i++)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, collisions[i].transform.position - transform.position);
                for (int j = 0; j < hit.Length; j++)
                {
                    if (hit[j].collider.gameObject.GetComponents<Rigidbody2D>().Length > 0)
                    {
                        if (hit[j].collider.gameObject.Equals(collisions[i].gameObject))
                        {
                            if (collisions[i].GetComponent<Rigidbody2D>() != null)
                            {
                                Vector2 direction = collisions[i].transform.position - transform.position;
                                collisions[i].GetComponent<Rigidbody2D>().AddForce(direction.normalized * pushForce * GetDistanceMultiplier(collisions[i].transform.position));
                            }
                        }
                    }
                    else if (hit[j].collider.isTrigger) { }
                    else
                    {
                        break;
                    }
                }
            }
        }
        else if(random > 90 && 95 > random)
        {
            Debug.Log("inverted");
            //inverted controls
        }
        else if (random > 95 && 100 > random)
        {
            //light
        }
        else
        {
            List<Actor> actors = GetAllHitActors();
            foreach (Actor actor in actors)
            {
                if(actor.GetComponent<Player>() != null)
                {
                    actor.TakeDamage(1000);
                }
            }
        }
    }
}
