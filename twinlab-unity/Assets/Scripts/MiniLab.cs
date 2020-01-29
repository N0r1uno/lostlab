using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniLab : Interactable
{
    [Header("MiniLab Specific")]
    public List<MiniLabPotionElement> potions;

    public Sprite fullMiniLab;
    public Sprite emptyMiniLab;
    private bool empty;
    private new SpriteRenderer renderer;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        Reset();
    }

    public override void ShowMessage(bool val)
    {
        if (interactionSprite != null && !empty)
            interactionSprite.enabled = val;
    }

    public override void Interact()
    {
        //PotionMixer.TogglePotionMixer(); WIP
        if (!empty)
        {
            foreach (MiniLabPotionElement mlpe in potions)
                Inventory.AddToCountOfPotion(mlpe.type, mlpe.count);

            renderer.sprite = emptyMiniLab;
            empty = true;
            interactionSprite.enabled = false;
        }
    }

    public void Reset()
    {
        empty = false;
        renderer.sprite = fullMiniLab;
    }

    [System.Serializable]
    public struct MiniLabPotionElement
    {
        public Potion.Type type;
        public int count;
    }
}
