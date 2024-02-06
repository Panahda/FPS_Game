using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    // IM2073 Project
    [SerializeField] private float bombDamage = 10.0f;

    [SerializeField] private GameObject explosiveParticle = null;

    [SerializeField] private HealthController _healthController = null;

    [SerializeField] private AudioClip bombAudio = null;
    private bool playingAudio;
    private AudioSource bombAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        bombAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            explosiveParticle.SetActive(true);
            bombAudioSource.PlayOneShot(bombAudio);
            _healthController.currentPlayerHealth -= bombDamage;
            _healthController.TakeDamage();
            gameObject.GetComponent<BoxCollider>().enabled = false;
            playingAudio = false;
        }
    }

    private void Update()
    {
        if(playingAudio)
        {
            if (bombAudioSource.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
    }
    // End Code
}
