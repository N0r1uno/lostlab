using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, collision.transform.position - transform.position);
        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].collider.gameObject.GetComponents<Rigidbody2D>().Length > 0)
            {
                if (hit[j].collider.gameObject.Equals(collision.gameObject))
                {
                    if (collision.GetComponent<Rigidbody2D>() != null)
                    {
                        collision.GetComponent<Rigidbody2D>().gravityScale = 0;
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


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
