using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float verticalSpeed = 2f;
    public float swayAmplitude = 1f;
    public float swayFrequency = 2f;

    private float startX;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startX = transform.position.x;

        // Randomize the sway values for each balloon
        swayAmplitude = Random.Range(0.5f, 1f);
        swayFrequency = Random.Range(0.5f, 1f);

        // Destroy the balloon after a random amount of time
        Destroy(gameObject, Random.Range(3f, 7f));
    }

    void FixedUpdate()
    {
        // Vertical movement handled by Rigidbody2D's physics
        rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);

        // Swaying on the X-axis
        float newX = startX + Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;

        // Apply the sway on the X axis while preserving the vertical velocity
        rb.position = new Vector2(newX, rb.position.y);
    }
}

