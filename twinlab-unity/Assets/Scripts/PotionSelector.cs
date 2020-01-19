using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PotionSelector : MonoBehaviour
{
    private PotionElement selectedElement, leftElement, rightElement;

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

        selectedElement = potions[0];
        leftElement = GetLeftNeighbourOf(selectedElement);
        rightElement = GetRightNeighbourOf(selectedElement);
        AssignSprites();

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

    private void AssignSprites()
    {
        left.sprite = leftElement.sprite;
        selected.sprite = selectedElement.sprite;
        right.sprite = rightElement.sprite;
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
        rightElement = selectedElement;
        selectedElement = leftElement;
        leftElement = GetLeftNeighbourOf(leftElement);
        AssignSprites();
    }
    public void ScrollLeft()
    {
        Debug.Log("Scroll left");
        leftElement = selectedElement;
        selectedElement = rightElement;
        rightElement = GetRightNeighbourOf(rightElement);
        AssignSprites();
    }

    [Serializable]
    public struct PotionElement
    {
        public Potion.Type type;
        public Sprite sprite;
    }
}