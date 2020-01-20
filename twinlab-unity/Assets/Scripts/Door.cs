using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool open;

    public bool completed = false;

    public bool timed;
    public float timeTillClosed;

    private float currentTime;

    public float speed;

    public Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        if (open)
        {
            Open();
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            if (open)
            {
                collider.enabled = false;
                completed = true;
            }
            else
            {
                collider.enabled = true;
                completed = true;
            }
        }
        if (timed && currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0f)
            {
                Close();
            }
        }
    }

    public void Open()
    {
        completed = false;
        open = true;
        if (timed)
        {
            currentTime = timeTillClosed;
        }
    }

    public void Close()
    {
        completed = false;
        open = false;
        currentTime = 0f;
    }

    public void ChangeState()
    {
        if (open)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
