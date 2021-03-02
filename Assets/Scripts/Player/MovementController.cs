using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float speedIncrease;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float driftForceIncrease;
    [SerializeField] private float steering;

    private Rigidbody2D rb;


    private float X;
    private float Y;

    private int _turnCount;
    private bool isTurn;

    public int TurnCount {
        get => _turnCount;
        set => _turnCount = value;
    }

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.Y = 1;
        this.X = 0;
    }

    private void Update()
    {
        Vector2 speed = transform.up * Y * speedIncrease;
        rb.AddForce(speed);

        float dir = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        if (dir > 0)
        {
            rb.rotation -= X * steering * (rb.velocity.magnitude / maxSpeed);
            if (!isTurn)
            {
                TurnCount++;
                isTurn = true;
            }
        }
        else {
            isTurn = false;
        }


        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right)) * driftForceIncrease * Time.deltaTime;
        Vector2 relativeForce = Vector2.left * driftForce;

        rb.AddForce(rb.GetRelativeVector(relativeForce));

        if (rb.velocity.magnitude > maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);
    }

    public void SetX(float value) => this.X = value;

    public void IncreaseMaxSpeed(float value) => this.maxSpeed += value;

    public void ResetPlayer() {
        if (this.rb != null)
        {
            this.rb.Sleep();
            this.rb.WakeUp();
            this.rb.velocity = new Vector2(0, 0);
            this.rb.rotation = 0;
            this.rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.rb.angularDrag = 0;
            this.rb.angularVelocity = 0;
        }
        this.TurnCount = 0;
    }
}
