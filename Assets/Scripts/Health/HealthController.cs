using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class HealthController : MonoBehaviour
{

    // IM2073 Project
    [Header("Player Health Amount")]
    public float currentPlayerHealth = 100.0f;
    [SerializeField] private float maxPLayerHealth = 100.0f;
    [SerializeField] private int regenRate = 1;
    private bool canRegen = false;

    [Header("Add the splatter image here")]
    [SerializeField] private Image redSplatterImage = null;

    [Header("Hurt Image Flash")]
    [SerializeField] private Image hurtImage = null;
    [SerializeField] private float hurtTimer = 0.1f;

    [Header("HealTimer")]
    [SerializeField] private float healCooldown = 4.0f;
    [SerializeField] private float maxHealCooldown = 4.0f;
    [SerializeField] private bool startCooldown = false;

    [Header("Audio Name")]
    [SerializeField] private AudioClip hurtAudio = null;
    private AudioSource healthAudioSource;

    public Camera cam;
    public Animator animator;
    public InputManager inputManager;
    public PlayerLook look;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        healthAudioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void UpdateHealth()
    {
        Color splatterAlpha = redSplatterImage.color;
        splatterAlpha.a = 1 - (currentPlayerHealth / maxPLayerHealth);
        redSplatterImage.color = splatterAlpha;
    }

    IEnumerator HurtFlash()
    {
        hurtImage.enabled = true;
        healthAudioSource.PlayOneShot(hurtAudio);
        yield return new WaitForSeconds(hurtTimer);
        hurtImage.enabled = false;
    }

    public void TakeDamage()
    {
        if(currentPlayerHealth >= 0)
        {
            canRegen = false;
            StartCoroutine(HurtFlash());
            UpdateHealth();
            healCooldown = maxHealCooldown;
            startCooldown = true;
        }
        else
        {
            Debug.Log("Player Died");
            animator.SetBool("Dead", true);
            inputManager.setPlayerDead();
            inputManager.enabled = false;
            look.setCameraWhenPlayerDead();
            PlayerDied();
        }
    }

    private void Update()
    {
        if (startCooldown)
        {
            healCooldown -= Time.deltaTime;
            if(healCooldown <= 0)
            {
                canRegen = true;
                startCooldown = false;
            }
        }

        if (canRegen && currentPlayerHealth >= 0)
        {
            if(currentPlayerHealth <= maxPLayerHealth - 0.01)
            {
                currentPlayerHealth += Time.deltaTime * regenRate;
                UpdateHealth();
            }
            else
            {
                currentPlayerHealth = maxPLayerHealth;
                healCooldown = maxHealCooldown;
                canRegen = false;
            }
        }
    }
    public void PlayerDied()
    {
        // possible to add Death UI
        StartCoroutine(ReturnToMainAfterDelay(5)); // 5 seconds delay
    }

    private IEnumerator ReturnToMainAfterDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds); // Wait for 5 seconds
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene. Make sure the scene name matches your main menu scene name.
    }
}
// End Code