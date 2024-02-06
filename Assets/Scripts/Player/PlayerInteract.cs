using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // IM2073 Project
    private Camera cam;
    [SerializeField] // Makes the private variable accessible within the Unity editor without making them public
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        // Create a ray at the center of the camera, shooting outwards
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        // Variable to store our collision information
        RaycastHit hitInfo; 
        // Ray cast to center of screen
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            // Checking if our gameObject has an interactable component
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                // Store interactable to variable
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                // Update on screen text
                playerUI.UpdateText(interactable.promptMessage);
                // When player triggers interactable
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
// End Code