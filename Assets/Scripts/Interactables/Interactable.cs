using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // IM2073 Project
    // Add or remove an InteractionEvent component to this gameObject.
    public bool useEvents;
    // message displayed to player when looking at an interactable.
    [SerializeField]
    public string promptMessage;

    // This function will be called from our player.
    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }
    protected virtual void Interact()
    {
        // We won't have any code written in this function
        // This is a template function to be overridden by our subclasses
    }
}
// End Code