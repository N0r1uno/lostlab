using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrok : MonoBehaviour
{
    public Transform target;
    public float range;
    public float speed;
    public Rigidbody2D rb;
    int direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        findTarget();
        moveToTarget();
    }

    public void findTarget()
    {
        target = GameObject.FindObjectOfType<Player>().transform;
        if(Vector2.Distance(transform.position, target.position) > range)
        {
            target = null;
        }
    }

    public void moveToTarget()
    {
        if (target != null) {
            if (target.position.x - transform.position.x > 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
        }
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
}
