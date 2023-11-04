using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    [SerializeField] protected float moveLimit;
    [SerializeField] private float moveSpeed;

    [SerializeField] private BallController ballController;

    private Vector3 moveVector;
    private bool isSpeedup;

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
}
