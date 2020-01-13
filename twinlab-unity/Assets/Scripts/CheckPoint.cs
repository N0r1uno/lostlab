using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Interactable
{

    [Header("Check Point Specific")]
    public Sprite deactivatedCheckpoint;
    public Sprite activatedCheckpoint;
    public Light screenLight;

    private bool active;
    private new SpriteRenderer renderer;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        active = false;
        ShowMessage(false);
        renderer.sprite = deactivatedCheckpoint;
        screenLight.enabled = false;
    }

    public override void Interact()
    {
        CheckPointManager.SetCheckPoint(this);
        if (!active)
        {
            active = true;
            renderer.sprite = activatedCheckpoint;
            screenLight.enabled = true;
        }
            
    }

    override public void ShowMessage(bool val)
    {
        
    }
}
