using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    public float min;
    public float max;
    [Range(0, 1)]
    public float threshold;

    private new Light light;
    private float t;
    private float multiplier;

    void Start()
    {
        light = GetComponent<Light>();
        t = Random.Range(0, threshold);
        multiplier = 1f;
    }

    void Update()
    {
        t -= Time.deltaTime;
        if (t <= 0)
        {
            light.intensity = Random.Range(min * multiplier, max * multiplier);
            t = Random.Range(0, 1f - threshold);
        }
    }

    public void SetMultiplier(float multiplier)
    {
        this.multiplier = multiplier;
    }
}