using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthSlider : MonoBehaviour
{
    Slider slider;

    private static HealthSlider instance;
    void Start()
    {
        slider = GetComponent<Slider>();
        instance = this;
        slider.value = 1f;
    }

    public static void SetValue(float val)
    {
        //Debug.Log("HealthSlider value " + val);
        instance.slider.value = val;
    }
}
