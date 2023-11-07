using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamageDealer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
