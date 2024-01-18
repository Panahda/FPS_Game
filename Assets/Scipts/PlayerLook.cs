using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRoatation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Calculate camera rotation for looking up and down
        xRoatation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRoatation = Mathf.Clamp(xRoatation, -80f, 80f);

        // Apply this to camera tranfrom
        cam.transform.localRotation = Quaternion.Euler(xRoatation, 0, 0);

        // Rrotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
