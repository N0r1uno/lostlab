using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PotionSelector : MonoBehaviour
{
    private PotionElement selectedElement;

    [Header("Potion Elements")]
    public List<PotionElement> potions;
    public Vector3 initialScale = Vector3.one * 0.8f;
    public Vector3 selectedScale = Vector3.one;

    Hashtable mappedPotions;
    static PotionSelector instance;
    void Start()
    {
        if (potions.Count == 0)
            Debug.LogError("Potion Selector Elements not assigned!");

        mappedPotions = new Hashtable();
        //setup
        foreach (PotionElement potion in potions)
        {
            mappedPotions.Add(potion.type, potion);
            potion.image.sprite = potion.sprite;
            potion.image.transform.localScale = initialScale;
            potion.SetCount(1);
        }
        selectedElement = potions[0];
        selectedElement.image.transform.localScale = selectedScale;
        instance = this;
    }

    public static Potion.Type GetSelectedPotionType()
    {
        return instance.selectedElement.type;
    }

    public static List<PotionElement> GetAllPotionElements()
    {
        return instance.potions;
    }

    public static PotionElement GetPotionElementOfType(Potion.Type t)
    {
        return (PotionElement) instance.mappedPotions[t];
    }

    public static PotionElement GetSelectedPotionElement()
    {
        return instance.selectedElement;
    }

    PotionElement GetRightNeighbourOf(PotionElement element)
    {
        int pos = potions.IndexOf(element);
        if (pos + 1 > potions.Count - 1)
            return potions[0];
        else return potions[pos + 1];
    }

    PotionElement GetLeftNeighbourOf(PotionElement element)
    {
        int pos = potions.IndexOf(element);
        if(pos == 0)
            return potions[potions.Count - 1];
        else
            return potions[pos - 1];
    }

    public static void ScrollRight()
    {
        //Debug.Log("Scroll right");
        instance.selectedElement.image.transform.localScale = instance.initialScale;
        instance.selectedElement = instance.GetRightNeighbourOf(instance.selectedElement);
        instance.selectedElement.image.transform.localScale = instance.selectedScale;
    }
    public static void ScrollLeft()
    {
        //Debug.Log("Scroll left");
        instance.selectedElement.image.transform.localScale = instance.initialScale;
        instance.selectedElement = instance.GetLeftNeighbourOf(instance.selectedElement);
        instance.selectedElement.image.transform.localScale = instance.selectedScale;
    }

    [Serializable]
    public class PotionElement
    {
        public Potion.Type type;
        public Sprite sprite;
        public Image image;
        public Text text;
        public GameObject prefab;
        private int count = 0;

        public int GetCount()
        {
            return count;
        }
        public void SetCount(int i)
        {
            count = i;
            text.text = count + "x";
        }
    }
}