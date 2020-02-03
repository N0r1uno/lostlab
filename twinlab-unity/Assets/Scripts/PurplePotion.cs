using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePotion : Potion
{
    [Range(0f, 1f)]
    public float percentDamage;

    [Header("Gravity Effect")]
    public GameObject gravityFieldPrefab;

    [Header("Push Effect")]
    public float pushForce;

    [Header("Invert Effect")]
    public float invertedTime;

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
        if (random > 0 && 20 > random)
            FreezeEffect();
        else if (random > 20 && 40 > random)
            FireEffect();
        else if (random > 40 && 60 > random)
            PoisonEffect();
        else if (random > 60 && 80 > random)
            PowerEffect();
        else if (random > 80 && 85 > random)
            GravityEffect();
        else if (random > 85 && 90 > random)
            ForceEffect();
        else if (random > 90 && 95 > random)
            InvertedEffect();
        else if (random > 95 && 100 > random)
            LightEffect();
        else
            InstakillEffect();
    }

    private void ForceEffect()
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

    private void InvertedEffect()
    {
        Debug.Log("inverted");
        List<Actor> actors = GetAllHitActors();
        foreach (Actor actor in actors)
            actor.InvertControls(invertedTime);
    }

    private void LightEffect()
    {
        Instantiate(lightPrefab, new Vector3(transform.position.x, transform.position.y, lightPrefab.transform.position.z), Quaternion.identity);
    }

    private void InstakillEffect()
    {
        List<Actor> actors = GetAllHitActors();
        foreach (Actor actor in actors)
        {
            if (actor.GetComponent<Player>() != null)
            {
                actor.TakeDamage(1000);
            }
        }
    }

    private void FireEffect()
    {
        FirePotion fire = Instantiate(Inventory.GetPotionPrefab(Potion.Type.fire), transform.position, Quaternion.identity).GetComponent<FirePotion>();
        fire.damage *= percentDamage;
        fire.SimulateCollision();
    }

    private void PoisonEffect()
    {
        PoisonPotion poison = Instantiate(Inventory.GetPotionPrefab(Potion.Type.poison), transform.position, Quaternion.identity).GetComponent<PoisonPotion>();
        poison.damage *= percentDamage;
        poison.SimulateCollision();
    }

    private void PowerEffect()
    {
        PowerPotion power = Instantiate(Inventory.GetPotionPrefab(Potion.Type.power), transform.position, Quaternion.identity).GetComponent<PowerPotion>();
        power.damage *= percentDamage;
        power.SimulateCollision();
    }

    private void FreezeEffect()
    {
        FreezePotion freeze = Instantiate(Inventory.GetPotionPrefab(Potion.Type.freeze), transform.position, Quaternion.identity).GetComponent<FreezePotion>();
        freeze.damage *= percentDamage;
        freeze.maxFreezeTime *= percentDamage;
        freeze.SimulateCollision();
    }

    private void GravityEffect()
    {
        Instantiate(gravityFieldPrefab, new Vector3(transform.position.x, transform.position.y, gravityFieldPrefab.transform.position.z), Quaternion.identity);
    }

}
