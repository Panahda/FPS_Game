using UnityEngine;
using UnityEngine.UI; // Import the UI namespace

public class StaminaBar : MonoBehaviour
{
    public PlayerMotor playerMotor; // Reference to the PlayerMotor script
    public Slider staminaSlider; // Reference to the UI Slider

    // Start is called before the first frame update
    void Start()
    {
        if (playerMotor != null && staminaSlider != null)
        {
            // Initialize the max value of the stamina slider to match the player's max stamina
            staminaSlider.maxValue = playerMotor.staminaMax;
            staminaSlider.value = playerMotor.staminaMax;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMotor != null && staminaSlider != null)
        {
            // Update the slider's value to match the player's current stamina
            staminaSlider.value = playerMotor.CurrentStamina;
        }
    }
}
