using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroReciever : MonoBehaviour
{
    public Sprite on;
    public Sprite off;

    public UnityEngine.Events.UnityEvent eventOn;

    public bool isOn;

    public void turnOn()
    {
        if (!isOn)
        {
            isOn = true;
            GetComponent<SpriteRenderer>().sprite = on;
            eventOn.Invoke();
        }
    }
}
