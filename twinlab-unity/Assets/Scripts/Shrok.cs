using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrok : Actor
{
    [Header("Shrok")]
    public float range;
    private Player target;

    
    private Vector3 origin;
    private Vector3 randomTarget;
    private float waitUntil;
    public float attackCooldown;
    private float cooldown;
    public float damage;
    void Start()
    {
        Initialize();
        target = FindObjectOfType<Player>();
        waitUntil = Time.time + Random.Range(0,5);
        origin = transform.position;
        NewRandomTarget();
    }

    void Update()
    {
        CalculateInput();
        ApplyMovement();
        ApplyAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if (cooldown <= 0)
            {
                cooldown = attackCooldown;
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            if (cooldown <= 0)
            {
                cooldown = attackCooldown;
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
    public void CalculateInput()
    {
        //player is in range! attac
        if (Vector2.Distance(transform.position, target.transform.position) <= range)
        {
            float p = target.transform.position.x - transform.position.x;
            input.SetHorizontal( Mathf.Abs(p) > 0.2f?(p > 0 ? 1 : -1) : 0 );
        }
        else if (Time.time > waitUntil)
        {
            float p = randomTarget.x - transform.position.x;
            if (Mathf.Abs(p) < 0.2f)
            {
                waitUntil = Time.time + Random.Range(5, 15);
                NewRandomTarget();
                input.SetHorizontal(0);
            } else input.SetHorizontal(p > 0 ? 1 : -1);
        }
    }      

    private void NewRandomTarget()
    {
        randomTarget = origin + Vector3.right*Random.Range(-range, range);
    }
}
