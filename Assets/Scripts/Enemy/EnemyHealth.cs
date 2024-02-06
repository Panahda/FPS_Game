using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // IM2073 Project
    [Header("Health Amount")]
    public float currentHealth = 50.0f;
    [SerializeField] private float maxHealth = 50.0f;

    StateMachine stateMachine;

    public float dealthWaitTime = 10f;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (currentHealth <= 0)
        {
            stateMachine.deadState = true;
            timer += Time.deltaTime;

            if(timer > dealthWaitTime)
            {
                Destroy(gameObject);
            }
        }
    }

}
// End Code