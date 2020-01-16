using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Collider2D collider;
    private bool thrown;
    private float time = 0.1f;
    public void Throw()
    {
        thrown = true;
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
