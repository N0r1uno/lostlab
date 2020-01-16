using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    public ParticleSystem ps;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collider.enabled = false;
        ps.gameObject.SetActive(true);
        ps.loop = false;
        ps.Play();
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<SpriteRenderer>().sprite = null;
    }

    
}
