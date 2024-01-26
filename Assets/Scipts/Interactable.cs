using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // message displayed to player when looking at an interactable.
    public string promptMessage;

    // This function will be called from our player.
    public void BaseInteract()
    {
        Interact();
    }
    protected virtual void Interact()
    {
        // We won't have any code written in this function
        // This is a template function to be overridden by our subclasses
    }
}
