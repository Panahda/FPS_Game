using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // IM2073 Project
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    private bool canMove = true;

    public void ProcessLook(Vector2 input)
    {
        if(canMove)
        {
            float mouseX = input.x;
            float mouseY = input.y;

            // Calculate camera rotation for looking up and down
            xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            // Apply this to camera tranfrom
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            // Rrotate player to look left and right
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
        }
    }

    public void setCameraWhenPlayerDead()
    {
        canMove = false;

        // Set camera's rotation to look down at the player
        cam.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
// End Code