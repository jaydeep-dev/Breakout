using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, IDamagable
{
    [field: SerializeField] public int Health { get; private set; } = 1;

    private SpriteRenderer spriteRenderer;
    private float alphaChange;

    public static event System.Action<Vector3> OnAnyBlockDestroyed;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alphaChange = 1f / Health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnAnyBlockDestroyed?.Invoke(transform.position);
            Destroy(gameObject);
        }
        else
        {
            var color = spriteRenderer.color;
            color.a -= alphaChange;
            spriteRenderer.color = color;
            Debug.Log(color.ToString() + alphaChange);
        }
    }
}
