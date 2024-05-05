using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ladderMovement : MonoBehaviour
{
    private float vertical; // Vertical movement

    private bool isLadder; // bool for if player is touch ladderz
    private bool isClimbing; // bool for if player is climbing the laddz

    [SerializeField] private float speed = 8f;          // Speed of ladder climbing

    [SerializeField] private Rigidbody2D rb;            // ref to the Rigid body
    private void Update()
    {
        vertical = Input.GetAxis("Vertical"); // set vertical varible input for vertical

        if (isLadder && Mathf.Abs(vertical) > 0f) // check if the player is on ladder & if their vertical is greater than 0
        { 
            isClimbing = true; // set is climbing
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing) // if climbing
        {
            rb.gravityScale = 0f; // set gravity to 0
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed); // set new vertical movement to include ladder speed
        }
        else 
        {
            rb.gravityScale = 4f; // set gravity to 4 if no climb ladder
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // once enter 2d collider
    {
        if (collision.CompareTag("Ladder")) // check ladder
        { 
            isLadder = true; // set ladder
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // once leave
    {
        if (collision.CompareTag("Ladder")) // was ladder?
        {
            isLadder = false; //  set no ladder
            isClimbing = false; //  rmeove climbving
        }
    }
}
