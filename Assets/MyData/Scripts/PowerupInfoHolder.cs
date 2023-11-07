using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInfoHolder : MonoBehaviour
{
    [field: SerializeField] public PowerupManager.Powerups powerup { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Void"))
        {
            Destroy(gameObject);
        }
    }
}
