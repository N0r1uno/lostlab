using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionMixer : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject panel;

    [Header("Recipes")]
    public List<PotionRecipe> recipes;

    static PotionMixer instance;
    void Start()
    {
        instance = this;
        panel.SetActive(false);
    }

    public static void TogglePotionMixer()
    {
        instance.panel.SetActive(!instance.panel.activeSelf);
        Time.timeScale = instance.panel.activeSelf?0:1;
    }

    private static PotionRecipe GetRecipe(Item a, Item b)
    {
        foreach (PotionRecipe pr in instance.recipes)
            if (pr.Equals(a, b))
                return pr;
        return null;
    }

    public static bool IsMixable(Item a, Item b)
    {
        return GetRecipe(a, b) == null;
    }

    public static void Mix(Item a, Item b)
    {
        PotionRecipe pr = GetRecipe(a, b);
        if (pr != null)
            Mix(pr);
    }

    public static void Mix(PotionRecipe pr)
    {
        Inventory.AddToCountOfPotion(pr.result.type, 1);
    }

    [System.Serializable]
    public class PotionRecipe
    {
        public Item a;
        public Item b;
        public Potion result;

        public bool Equals(Item a, Item b)
        {
            return this.a == a && this.b == b;
        }
    }
}
