using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Elements")]
    public List<InventoryElement> inventoryElements;

    static Inventory instance;
    void Start()
    {
        instance = this;
    }

    public struct InventoryElement
    {
        public Sprite sprite;
        public int count;
    }
}
