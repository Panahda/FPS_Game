using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class AttackState : BaseState
{
    // IM2073 Project
    private float moveTimer;
    private float losePlayerTimer;
    public float waitBeforeSearchTime = 8.0f;
    private float shotTimer;
    Animator animator;
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer()) // if player can be seen
        {
            // lock the lose player timer and increment the move and shot timer
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            // if shot timer > fireRate
            if(shotTimer > enemy.fireRate)
            {
                Shoot();
            }

            if(moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
            enemy.LastKnowPos = enemy.Player.transform.position;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > waitBeforeSearchTime)
            {
                // change to search state
                stateMachine.ChangeState(new SearchState ());
            }
        }
    }

    public void Shoot()
    {
        // Store reference to gun barrel
        Transform gunbarrel = enemy.gunBarrel;

        // Instantiate a new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);

        // Calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;

        // Add force rigidbody of the bullet
        bullet.GetComponent<Rigidbody>().velocity = UnityEngine.Quaternion.AngleAxis(Random.Range(-3f,3f), UnityEngine.Vector3.up) * shootDirection * 40;

        Debug.Log("Shoot: " + shootDirection);
        shotTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
// End Code