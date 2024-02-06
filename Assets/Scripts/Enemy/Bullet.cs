using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // IM2073 Project
    private float bulletTime = 0f;
    [SerializeField] private float bulletDamage = 10.0f;
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Player"))
        {
            HealthController _healthController = collision.transform.GetComponentInChildren<HealthController>();
            Debug.Log("Hit Player");
            _healthController.currentPlayerHealth -= bulletDamage;
            _healthController.TakeDamage();
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