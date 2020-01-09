using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    virtual public void Interact()
    {
        Debug.Log("Interacting with " + this.gameObject.name);
    }

    virtual public void ShowMessage(bool val)
    {
        Debug.Log("Showing Interactable Message for " + this.gameObject.name);
    }
}
