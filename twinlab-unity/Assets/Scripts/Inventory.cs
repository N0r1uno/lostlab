using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Items")]
    //hardgecodet
    public int freezePotion;

    static Inventory instance;
    void Start()
    {
        instance = this;
    }

}
