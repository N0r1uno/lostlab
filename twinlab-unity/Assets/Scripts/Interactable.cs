using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent interacted;
    public SpriteRenderer interactionSprite;

    virtual public void Interact()
    {
        interacted.Invoke();
        Debug.Log("Interacting with " + this.gameObject.name);
    }

    virtual public void ShowMessage(bool val)
    {
        Debug.Log("Showing Interactable Message for " + this.gameObject.name);
        if (interactionSprite != null)
        {
            interactionSprite.enabled = val;
        }
    }
}
