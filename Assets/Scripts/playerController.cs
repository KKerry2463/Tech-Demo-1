using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private float horizontal; // Horizontal Input ref

    private bool isFacingRight = true; // Take a guess!

    [SerializeField] private float speed = 8f; // Horizontal Speed
    [SerializeField] private float jumpPower = 16f; // Vertical jump strenght

    [HideInInspector] public bool isThrown = false; // Check if something if thrown.
    public GameObject boomerang; // refernce to boomerang

    private bool jetpackActive = false;
    [SerializeField] private float jetpackBoost = 30f; // jetpack boost force
    [SerializeField] private GameObject jetpackVisual; // visual jetpack element

    [SerializeField] private Rigidbody2D rb;            // Rigidbody refernce
    [SerializeField] private Transform groundCheck;     // groundchecker refernce
    [SerializeField] private LayerMask groundLayer;     // grounlayer refernce  

    private bool onMovingPlatform = false;
    private GameObject movingPlatformObject;
    private Vector2 movingPlatformPreviousPosition;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); //Set horizontal float off raw input axis.

        if (Input.GetButtonDown("Jump") && IsGrounded()) // is player on the ground & pressing jump
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower); // change Y of rigidbody to our jumpPower
        }

        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0.1f && jetpackActive) // Is the player holding Jump while velocity is above 0
        {
            rb.AddForce(rb.transform.up * jetpackBoost, ForceMode2D.Impulse);
        }

        Flip(); // Call flip method

        if (Input.GetMouseButtonDown(0) && !isThrown) // if left click and nothing is thrown then
        {
            throwObject(); // throw method time...
        }

        if (onMovingPlatform)
        {
            Vector2 platformDelta = (Vector2)movingPlatformObject.transform.position - movingPlatformPreviousPosition;
            rb.position += platformDelta;
        }

        movingPlatformPreviousPosition = movingPlatformObject.transform.position;
    }
    private void FixedUpdate()
    {
        if (!onMovingPlatform)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // change the horizontal movespeed.
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed + (movingPlatformObject.transform.position.x - movingPlatformPreviousPosition.x) / Time.fixedDeltaTime, rb.velocity.y); // Adjust player velocity according to platform velocity
        }
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
        boomerang.SetActive(true); // Activate the boomerang

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get the mouse position in world coordinates
        boomerang.GetComponent<boomerang>().destination = mousePosition;
        boomerang.GetComponent<boomerang>().SetPlayer(gameObject);  // Set the player game object in the boomeran
        boomerang.GetComponent<boomerang>().transform.position = gameObject.transform.position;
        isThrown = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();

        if (jetpack != null)
        {
            jetpackActive = true;
            jetpackVisual.SetActive(jetpackActive);
            Destroy(jetpack.gameObject);
        }

        MovingPlatform movingPlatform = collision.gameObject.GetComponent<MovingPlatform>();

        if (movingPlatform != null)
        {
            onMovingPlatform = true;
            movingPlatformObject = movingPlatform.gameObject;
            movingPlatformPreviousPosition = movingPlatformObject.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        MovingPlatform movingPlatform = collision.gameObject.GetComponent<MovingPlatform>();
        if (movingPlatform != null)
        {
            onMovingPlatform = false;
            movingPlatformObject = null;
        }
    }
}
