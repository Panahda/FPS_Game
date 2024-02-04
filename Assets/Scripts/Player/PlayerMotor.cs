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
    public float sprintMax = 8f;
    public float restMax = 5f;
    public Transform gunBarrel;
    public Camera playerCamera;

    bool crouching = false;
    float crouchTimer = 1;
    bool lerpCrouch = false;
    float sprintTimer;
    float restTimer;
    bool sprinting;



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
        sprintTimer = 0;
        restTimer = 0;
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
            sprintTimer += Time.deltaTime;
            // Debug.Log(sprintTimer);
            if (sprintTimer > sprintMax)
            {
                sprintTimer = 0f;
                canSprint = false;
                audioSource.PlayOneShot(outOfBreathAudio);
            }
        }
        else
        {
            restTimer += Time.deltaTime;
        }

        if(restTimer > restMax)
        {
            restTimer = 0f;
            canSprint = true;
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
            speed = 8;
            sprinting = true;
        }
    }

    public void StopSprint()
    {
        speed = 5;
        sprintTimer = 0;
        sprinting = false;
    }

    public void Shoot()
    {
        shootingSound.Play();
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/PlayerBullet") as GameObject, gunBarrel.position, playerCamera.transform.rotation);
        
        bullet.GetComponent<Rigidbody>().velocity = UnityEngine.Quaternion.AngleAxis(Random.Range(-1f, 1f), UnityEngine.Vector3.up) * playerCamera.transform.forward * 50;
    }
}
