using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Actor
{
    [Header("Slime")]
    public float range;
    public float attackRange;
    public float damage;
    public float cooldown;
    public Collider2D collider;
    public ParticleSystem ps;
    private float currentCooldown;
    private Player target;

    private Vector3 origin;
    private Vector3 randomTarget;
    private float waitUntil;
    void Start()
    {
        ps.Stop();
        Initialize();
        target = FindObjectOfType<Player>();
        waitUntil = Time.time + Random.Range(0, 5);
        origin = transform.position;
        NewRandomTarget();
    }

    void Update()
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        CalculateInput();
        ApplyMovement();
        ApplyAnimation();
        if(target != null)
        {
            ps.transform.LookAt(target.transform);
        }
    }

    public void DealDamage()
    {
        if (currentCooldown <= 0)
        {
            // Is in attackRange
            if (Vector2.Distance(target.transform.position, transform.position) <= attackRange)
            {
                Debug.Log("test");
                RaycastHit2D[] hit = new RaycastHit2D[1];
                collider.Raycast(target.transform.position - transform.position, hit);
                Debug.DrawRay(transform.position, target.transform.position - transform.position);
                if (hit[0].collider.GetComponent<Player>())
                {
                    Debug.Log("test2");
                    currentCooldown = 0;
                    target.TakeDamage(damage);
                    ps.time = 0;
                    ps.Play();
                }
            }
        }
    }

    public void CalculateInput()
    {

        //player is in range! attac
        if (Vector2.Distance(transform.position, target.transform.position) <= range)
        {
            float p = target.transform.position.x - transform.position.x;
            input.SetHorizontal(Mathf.Abs(p) > 0.2f ? (p > 0 ? 1 : -1) : 0);
            //slime is jumping
            input.isJumping = true;
            DealDamage();
        }
        else if (Time.time > waitUntil)
        {
            float p = randomTarget.x - transform.position.x;
            if (Mathf.Abs(p) < 0.2f)
            {
                waitUntil = Time.time + Random.Range(5, 15);
                NewRandomTarget();
                input.SetHorizontal(0);
                //slime is jumping
                input.isJumping = false;
            }
            else
            {
                input.SetHorizontal(p > 0 ? 1 : -1);
                input.isJumping = true;
            }
        }
    }

    private void NewRandomTarget()
    {
        randomTarget = origin + Vector3.right * Random.Range(-range, range);
    }
}
