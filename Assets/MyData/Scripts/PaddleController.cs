using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    [SerializeField] protected float moveLimit;
    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject laserGun;

    [SerializeField] private BallController ballController;

    private Vector3 moveVector;
    private bool isSpeedup;

    public void OnThrow(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && GetComponentInChildren<BallController>())
        {
            ballController.ThrowBall();
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveVector = ctx.ReadValue<float>() * Vector3.right;
    }

    public void OnSpeedUp(InputAction.CallbackContext ctx)
    {
        isSpeedup = ctx.action.triggered;
    }

    private void Update()
    {
        HandleMovement();
        HandleGameSpeedUp();
    }

    private void HandleGameSpeedUp()
    {
        Time.timeScale = isSpeedup ? 2 : 1;
    }

    private void HandleMovement()
    {
        transform.Translate(moveSpeed * Time.deltaTime * moveVector);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -moveLimit, moveLimit), transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.transform.TryGetComponent<PowerupInfoHolder>(out var powerupInfo))
        {
            switch (powerupInfo.powerup)
            {
                case PowerupManager.Powerups.Laser:
                    EnableLaserGun();
                    break;

                case PowerupManager.Powerups.Multi:
                    ballController.SpawnBalls();
                    break;

                default:
                    Debug.LogError("Powerup Doesn't Exist");
                    break;
            }

            Destroy(powerupInfo.gameObject);
        }
    }

    private void EnableLaserGun()
    {
        laserGun.SetActive(true);
    }
}
