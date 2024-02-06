using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    // IM2073 Project
    Animator animator;
    Enemy enemy;
    EnemyHealth health;

    private string currentState;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {

        if((enemy.currentState == "PatrolState") || (enemy.currentState == "SearchState"))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if((enemy.currentState == "AttackState"))
        {
            animator.SetTrigger("shoot");
        }

        if(health.currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }
}
// End Code