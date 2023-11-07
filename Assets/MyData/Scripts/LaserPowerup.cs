using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserPowerup : MonoBehaviour
{
    [SerializeField] private int maxShots = 10;
    [SerializeField] private GameObject laserPrefab;

    private int shotsFired = 0;

    private void OnEnable()
    {
        LeanTween.moveLocalY(gameObject, 1, .5f);
        LeanTween.delayedCall(gameObject, 10f, () => DisableGun());
    }

    private void DisableGun()
    {
        LeanTween.cancel(gameObject);
        LeanTween.moveLocalY(gameObject, 0, .5f).setOnComplete(() =>
        {
            shotsFired = 0;
            gameObject.SetActive(false);
        });
    }

    public void OnFireLaser(InputAction.CallbackContext ctx)
    {
        if(isActiveAndEnabled && ctx.action.triggered && shotsFired < maxShots)
        {
            var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            var rb = laser.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.up * 50f;
            shotsFired++;

            if (shotsFired >= maxShots)
                DisableGun();
        }
    }
}
