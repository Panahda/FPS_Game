using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float staminaMax = 5f;
    public float staminaCoolDown = 1f;
    public Transform gunBarrel;
    public Camera playerCamera;

    bool crouching = false;
    float crouchTimer = 1;
    bool lerpCrouch = false;
    bool sprinting;
    bool startStaminaCoolDown = false;
    float cooldown;

    [SerializeField]
    private float currentStamina;

    [SerializeField]
    private bool canSprint = true;

    [Header("Audio Name")]
    [SerializeField] private AudioClip outOfBreathAudio = null;
    private AudioSource audioSource;
    public AudioSource shootingSound;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        currentStamina = staminaMax;
        shootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if(p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

        if (sprinting)
        {
            currentStamina -= Time.deltaTime;
            startStaminaCoolDown = false;
            if (currentStamina <= 0)
            {
                canSprint = false;
                StopSprint();
                audioSource.PlayOneShot(outOfBreathAudio);
                startStaminaCoolDown = true;
                cooldown = 0;
            }
        }
        else
        {
            if(cooldown >= staminaCoolDown)
            {
                currentStamina += Time.deltaTime;
            }

            if((startStaminaCoolDown) && (cooldown <= staminaCoolDown))
            {
                cooldown += Time.deltaTime;
            }

            if(currentStamina >= staminaMax - 0.1f)
            {
                currentStamina = staminaMax;
                startStaminaCoolDown = false;
                canSprint = true;
            }
            startStaminaCoolDown = true;
        }


    }

    // Receive input from InputManager.cs and apply to character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        // Debug.Log("gravity: " + playerVelocity.y + " speed: " + speed);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        if (canSprint)
        {
            speed = 10;
            sprinting = true;
        }
        else
        {
            speed = 5;
            sprinting = false; 
        }
    }

    public void StopSprint()
    {
        speed = 5;
        sprinting = false;
    }

    public void Shoot()
    {
        shootingSound.Play();
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/PlayerBullet") as GameObject, gunBarrel.position, playerCamera.transform.rotation);
        
        bullet.GetComponent<Rigidbody>().velocity = UnityEngine.Quaternion.AngleAxis(Random.Range(-1f, 1f), UnityEngine.Vector3.up) * playerCamera.transform.forward * 50;
    }

    public float CurrentStamina
    {
        get { return currentStamina; }
    }
}
