using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private float horizontal; // Horizontal Input ref

    private bool isFacingRight = true; // Take a guess!

    [SerializeField] private float speed = 8f; // Horizontal Speed
    [SerializeField] private float jumpPower = 16f; // Vertical jump strenght

    public bool isThrown = false; // Check if something if thrown.
    [SerializeField] private GameObject boomerang; // refernce to boomerang
    [SerializeField] private Transform throwingArm; //Empty object used to throw things from.

    [SerializeField] private Rigidbody2D rb;            // Rigidbody refernce
    [SerializeField] private Transform groundCheck;     // groundchecker refernce
    [SerializeField] private LayerMask groundLayer;     // grounlayer refernce

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); //Set horizontal float off raw input axis.

        if (Input.GetButtonDown("Jump") && IsGrounded()) // is player on the ground & pressing jump
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower); // change Y of rigidbody to our jumpPower
        }

        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f) // Is the player holding Jump while out velocity is above 0?
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); // Add some extra force to the Y, Hold button go higher slightly.
        }

        Flip(); // Call flip method

        if (Input.GetMouseButtonDown(0) && !isThrown) // if left click and nothing is thrown then
        {
            throwObject(); // throw method time...
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // change the horizontal movespeed.
    }
    private void Flip() // the so called flip method
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) // if player is facting right and moving left OR is not facing right but is moving left then..
        {
            isFacingRight = !isFacingRight; // change bool
            Vector3 localScale = transform.localScale; // set local scale to new variable
            localScale.x *= -1f;  // Maths
            transform.localScale = localScale; // Set local scale to variable
        }
    }
    private bool IsGrounded() // Mythical is grounded method, No mum dont ground me!
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); // Returns like this "Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer)" Yeah I know I am genuis.
    }

    void throwObject()
    {
        Instantiate(boomerang, throwingArm.position, throwingArm.rotation);
        isThrown = true;
    }
}
