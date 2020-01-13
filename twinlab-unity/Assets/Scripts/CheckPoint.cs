using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Interactable
{
    
    void Start()
    {
        ShowMessage(false);
    }

    public override void Interact()
    {
        CheckPointManager.SetCheckPoint(this);
    }

    override public void ShowMessage(bool val)
    {
        
    }
}
