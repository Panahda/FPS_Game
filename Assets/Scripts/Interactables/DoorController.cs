using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private AudioClip slidingDoor = null;
    private AudioSource doorAudioSource;
    private Animator animator;

    private void start()
    {
        animator.SetBool("isOpen", false);
    }

    public void OpenDoor()
    {
        doorAudioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        
        Debug.Log("Opening Door");
        animator.SetBool("isOpen", true);
        doorAudioSource.PlayOneShot(slidingDoor);
    }
}