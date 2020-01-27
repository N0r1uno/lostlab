using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniLab : Interactable
{
    //[Header("MiniLab Specific")]
    //public List<>

    public override void Interact()
    {
        Inventory.AddToCountOfPotion(PotionSelector.GetSelectedPotionType(), 1);
        PotionMixer.TogglePotionMixer();

    }
}
