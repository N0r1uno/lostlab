using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fountain : MonoBehaviour
{
    public float force;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            float finalForce = force / collision.Distance(gameObject.GetComponent<Collider2D>()).distance;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            float finalForce = force / collision.Distance(gameObject.GetComponent<Collider2D>()).distance;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
        }
    }
}
