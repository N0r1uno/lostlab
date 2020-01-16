using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PotionSelector : MonoBehaviour
{
    public PotionElement currentSelection;

    [Header("UI Element")]
    public Image selected;
    public Image left;
    public Image right;
    public Text count;

    [Header("Potion Sprites")]
    public List<PotionElement> potions;

    static PotionSelector instance;
    void Start()
    {
        if (selected == null || left == null || right == null || count == null)
            Debug.LogError("Potion Selector UI Elements not assigned!");
        currentSelection = potions[0];
        instance = this;
    }

    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0f)
            if (scrollWheel > 0f)
                scrollRight();
            else
                scrollLeft();
    }

    public static Potion.Type getSelectedPotionType()
    {
        return instance.currentSelection.type;
    }

    public static void scrollRight()
    {
        Debug.Log("Scroll right");
        int i = instance.potions.IndexOf(instance.currentSelection);
        if (i + 1 > instance.potions.Count - 1) //richtig so?
            instance.currentSelection = instance.potions[0];
        else
            instance.currentSelection = instance.potions[i + 1];
        instance.selected.sprite = instance.currentSelection.sprite;
    }
    public static void scrollLeft()
    {
        Debug.Log("Scroll left");
        int i = instance.potions.IndexOf(instance.currentSelection);
        if (i == 0)
            instance.currentSelection = instance.potions[instance.potions.Count-1];
        else
            instance.currentSelection = instance.potions[i - 1];
        instance.selected.sprite = instance.currentSelection.sprite;
    }

    [Serializable]
    public struct PotionElement
    {
        public Potion.Type type;
        public Sprite sprite;
    }
}
