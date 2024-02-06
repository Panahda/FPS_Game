using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // IM2073 Project
    public BaseState activeState;
    public bool deadState = false;

    public void Initialise()
    {
        ChangeState(new PatrolState ());
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((activeState != null) && (deadState == false))
        {
            activeState.Perform();
        }
        else if (deadState == true)
        {
            activeState.Exit();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if(activeState != null)
        {
            // run cleanup on activeState
            activeState.Exit();
        }

        // cange to a new state
        activeState = newState;

        // fail-safe null check to make sure new state wasn't null
        if (activeState != null)
        {
            // Setup new state
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
// End Code