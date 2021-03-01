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

    private void Start()
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
        }
        else {
            rb.rotation += X * steering * (rb.velocity.magnitude / maxSpeed);
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
}
