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
            transform.Translate(Vector2.right * speed * Time.deltaTime); // Move the boomerang forward

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
    }
}
