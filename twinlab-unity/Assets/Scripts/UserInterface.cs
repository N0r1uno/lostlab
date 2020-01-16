using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public Slider healthSlider;

    static UserInterface instance;

    void Start()
    {
        instance = this;
    }

    public static void setHealth(float value)
    {
        instance.healthSlider.value = value;
    }


}
