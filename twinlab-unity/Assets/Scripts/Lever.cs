using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Lever : Interactable
{
    [Header("Lever Specific")]
    public bool isOn;
    public List<Sprite> sprites;
    public float speed = 0.1f;

    private new SpriteRenderer renderer;
    private bool isPlayingAnimation;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        if (isOn) renderer.sprite = sprites[0];
        else renderer.sprite = sprites[sprites.Count - 1];
    }

    public override void Interact()
    {
        if (!isPlayingAnimation)
        {
            base.Interact();
            Toggle();
        }
    }

    public void Toggle()
    {
            isOn = !isOn;
            if (isOn) StartCoroutine(LeverUp());
            else StartCoroutine(LeverDown());
    }

    IEnumerator LeverDown()
    {
        isPlayingAnimation = true;
        for (int i = 0; i < sprites.Count; i++)
        {
            renderer.sprite = sprites[i];
            yield return new WaitForSeconds(speed);
        }
        isPlayingAnimation = false;
    }

    IEnumerator LeverUp()
    {
        isPlayingAnimation = true;
        for (int i = sprites.Count - 1; i >= 0; i--)
        {
            renderer.sprite = sprites[i];
            yield return new WaitForSeconds(speed);
        }
        isPlayingAnimation = false;
    }

}
