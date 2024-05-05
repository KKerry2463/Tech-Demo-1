using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerang : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float returnSpeed = 10f;
    [SerializeField] private float maxDistance = 5f;

    private GameObject player;
    private Vector2 playerPosition;
    private Vector2 startPosition;
    private Vector2 throwPosition;
    private bool returning = false;

    private void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        playerPosition = player.transform.position;

        if (!returning)
        {
            if (throwPosition == Vector2.zero)
            {
                throwPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //  thow it towards mouse.
            }

            transform.position = Vector2.MoveTowards(transform.position, throwPosition, speed * Time.deltaTime); // Move the boomerang towards the throw position

            if (Vector2.Distance(startPosition, transform.position) >= maxDistance) // Check if the boomerang has reached its max distance
            {
                returning = true;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, returnSpeed * Time.deltaTime);   // Move the boomerang back to the player

            if ((Vector2)transform.position == playerPosition) // Check if the boomerang has returned to the player
            {
                GameObject.FindWithTag("Player").GetComponent<playerController>().isThrown = false; // If back at player set is thrown to false.
                Destroy(gameObject);
            }
        }

       
        if (!returning && Vector2.Distance(transform.position, throwPosition) <= 0.1f)  // Check if the boomerang has passed the throw position ARGH IM LOOSING MY FUCKING MIDNT HIS TINY LITTLE FUCKING BUG IS GOING CRAZY THIS EBTTER FUCKING FIX IT.
        {
            returning = true;
        }
    }
}
