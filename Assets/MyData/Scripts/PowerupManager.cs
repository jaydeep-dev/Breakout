using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public enum Powerups
    {
        Laser = 0,
        Multi = 1,
    }

    private Powerups powerup;
    [SerializeField] private List<PowerupInfo> powerupPrefabsList;

    private void OnEnable()
    {
        BoxController.OnAnyBlockDestroyed += OnAnyBlockDestroyed;
    }

    private void OnAnyBlockDestroyed(Vector3 pos)
    {
        if (Random.Range(1f, 100f) <= 10f)
            SpawnPowerup(pos);
    }

    private void SpawnPowerup(Vector3 pos)
    {
        powerup = (Powerups)Random.Range(0, powerupPrefabsList.Count);
        var selectedPowerup = powerupPrefabsList.Find(x => x.powerup == powerup);

        Instantiate(selectedPowerup.powerupPrefab, pos, Quaternion.identity);
    }

    private void OnDisable()
    {
        BoxController.OnAnyBlockDestroyed -= OnAnyBlockDestroyed;
    }
}

[System.Serializable]
public struct PowerupInfo
{
    public PowerupManager.Powerups powerup;
    public GameObject powerupPrefab;
}
