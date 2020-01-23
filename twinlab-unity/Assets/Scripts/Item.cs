using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    public new Collider2D collider;
    private bool thrown = false;
    private bool hasBeenThrown = false;
    private float time = 0.1f;

    public void Start()
    {
        if (collider == null)
            collider = GetComponent<Collider2D>();
    }

    public bool HasBeenThrown()
    {
        return hasBeenThrown;
    }

    public void Throw()
    {
        thrown = true;
        hasBeenThrown = true;
    }

    private void Update()
    {
        if (thrown)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                collider.enabled = true;
                thrown = false;
            }
        }
    }
}
