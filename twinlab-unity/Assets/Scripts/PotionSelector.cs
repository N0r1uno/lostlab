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
    static PotionSelector instance;
    void Start()
    {
        if (potions.Count == 0)
            Debug.LogError("Potion Selector Elements not assigned!");

        //setup
        foreach (PotionElement potion in potions)
        {
            potion.image.sprite = potion.sprite;
            potion.image.transform.localScale = initialScale;
        }
        selectedElement = potions[0];
        selectedElement.image.transform.localScale = selectedScale;
        instance = this;
    }

    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0f)
            if (scrollWheel > 0f)
                ScrollRight();
            else
                ScrollLeft();
    }

    public static Potion.Type GetSelectedPotionType()
    {
        return instance.selectedElement.type;
    }

    private PotionElement GetRightNeighbourOf(PotionElement element)
    {
        int pos = potions.IndexOf(element);
        if (pos + 1 > potions.Count - 1)
            return potions[0];
        else return potions[pos + 1];
    }

    private PotionElement GetLeftNeighbourOf(PotionElement element)
    {
        int pos = potions.IndexOf(element);
        if(pos == 0)
            return potions[potions.Count - 1];
        else
            return potions[pos - 1];
    }

    public void ScrollRight()
    {
        Debug.Log("Scroll right");
        selectedElement.image.transform.localScale = initialScale;
        selectedElement = GetRightNeighbourOf(selectedElement);
        selectedElement.image.transform.localScale = selectedScale;
    }
    public void ScrollLeft()
    {
        Debug.Log("Scroll left");
        selectedElement.image.transform.localScale = initialScale;
        selectedElement = GetLeftNeighbourOf(selectedElement);
        selectedElement.image.transform.localScale = selectedScale;
    }

    [Serializable]
    public struct PotionElement
    {
        public Potion.Type type;
        public Sprite sprite;
        public Image image;
        public Text count;
    }
}