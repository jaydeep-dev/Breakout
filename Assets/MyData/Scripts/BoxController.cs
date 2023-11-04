using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, IDamagable
{
    [field: SerializeField] public int Health { get; private set; } = 1;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            Destroy(gameObject);
    }
}
