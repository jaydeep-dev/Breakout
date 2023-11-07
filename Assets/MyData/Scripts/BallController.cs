using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private bool useRandomColor = false;
    [SerializeField] private float ballForce = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastVelocity;
    private Vector3 spawnPos;
    private bool applySpeedCheck;

    private ScoresManager scoresManager;
    private PaddleController paddleController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        paddleController = GetComponentInParent<PaddleController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spawnPos = transform.localPosition;
        scoresManager = FindObjectOfType<ScoresManager>();
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void FixedUpdate()
    {
        if (applySpeedCheck)
        {
            rb.velocity = lastVelocity * ballForce;
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, ballForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (useRandomColor)
            spriteRenderer.color = Random.ColorHSV();

        var reflectDir = Vector2.Reflect(lastVelocity, other.contacts[0].normal).normalized;
        //Debug.Log(lastVelocity + "----" + collision.contacts[0].normal + "----" + reflectDir);
        rb.velocity = Vector3.zero;
        rb.AddForce(reflectDir * ballForce, ForceMode2D.Impulse);

        if (other.transform.TryGetComponent( out IDamagable damagable))
        {
            damagable.TakeDamage(1);
            scoresManager.UpdateScore();
        }
        if (other.transform.CompareTag("Void"))
        {
            ResetBall();
        }
    }

    private void ResetBall()
    {
        Debug.Log("Reset Ball");
        lastVelocity = rb.velocity = Vector3.zero;
        if (paddleController)
            transform.SetParent(paddleController.transform, false);
        else
            Destroy(gameObject);
        transform.localPosition = spawnPos;
    }

    public void ThrowBall()
    {
        transform.SetParent(null);  
        var xDirection = Random.Range(-1f, 1f);
        var yDirection = 1f;
        Vector2 force = new Vector2(xDirection , yDirection).normalized * ballForce;
        rb.AddForce(force, ForceMode2D.Impulse);
        lastVelocity = rb.velocity;
        Debug.Log("Ball's Dir: " + force);

        applySpeedCheck = true; // Make sure this is the last line so after setting the velocity the fixed update starts velocity checks
    }

    public void SpawnBalls()
    {
        // aay badha ball mathi spawn karava na
        BallController[] balls = FindObjectsOfType<BallController>();
        Debug.Log("Existing Ball Count : " + balls.Length);
        foreach (var ball in balls)
        {
            var ball1 = Instantiate(gameObject, ball.transform.position, Quaternion.identity);
            var ball2 = Instantiate(gameObject, ball.transform.position, Quaternion.identity);

            ball1.GetComponent<BallController>().ThrowBall();
            ball2.GetComponent<BallController>().ThrowBall();
        }
    }
}
