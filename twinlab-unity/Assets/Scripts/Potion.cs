using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    public enum Type
    {
        freeze,
        poison,
        purple,
        fire,
        power
    }

    public Type type;
    public float range;
    public float damage;
    public GameObject particles;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collider.enabled = false;
        //particle system
        ParticleSystem ps = Instantiate(particles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        Destroy(ps.gameObject, ps.main.duration);
        //
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<SpriteRenderer>().sprite = null;
        Effect();
        Destroy(gameObject);
    }

    public virtual void Effect()
    {
        //do something
        Debug.Log("Potion destroyed");
    }

    public float GetDistanceMultiplier(Actor actor)
    {
        return Mathf.Clamp(1f - (Vector3.Distance(actor.transform.position, transform.position) / range), 0.1f, 1f);
    }

    public float GetDistanceMultiplier(Vector2 vector)
    {
        return Mathf.Clamp(1f - (Vector3.Distance(vector, transform.position) / range), 0.1f, 1f);
    }

    public List<Actor> GetAllHitActors()
    {
        List<Actor> allHitActors = new List<Actor>();
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, range);
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].gameObject.GetComponents<Actor>().Length > 0){
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, collisions[i].transform.position - transform.position);
                for(int j = 0; j < hit.Length; j++)
                {
                    if(hit[j].collider.gameObject.GetComponents<Actor>().Length > 0)
                    {
                        if (hit[j].collider.gameObject.Equals(collisions[i].gameObject))
                        {
                            allHitActors.Add(collisions[i].GetComponent<Actor>());
                        }
                    }else if (hit[j].collider.isTrigger) { } else
                    {
                        break;
                    }
                }
            }
        }
        return allHitActors;

    } 
}
