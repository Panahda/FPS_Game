using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersBullet : MonoBehaviour
{
    // IM2073 Project
    [SerializeField] private float bulletDamage = 10.0f;
    float bulletTime = 0f;
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Enemy"))
        {
            EnemyHealth _enemyHealth = collision.transform.GetComponentInChildren<EnemyHealth>();
            Debug.Log("Hit Enemy");
            _enemyHealth.currentHealth -= bulletDamage;
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        bulletTime += Time.deltaTime;
        if (bulletTime > 8)
        {
            Destroy(gameObject);
        }
    }
}
// End Code