using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    [SerializeField] private Transform start, end;
    [SerializeField] private float speed;
    Vector2 targetPos;

    playerController player;
    Rigidbody2D playerRb;
    Vector2 moveDirection;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        targetPos = end.position;
        DirectionCalculate();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, start.position) < 0.5f)
        {
            targetPos = end.position;
            DirectionCalculate();
        }

        if (Vector2.Distance(transform.position, end.position) < 0.5f)
        {
            targetPos = start.position;
            DirectionCalculate();
        }

    }
    private void FixedUpdate()
    {
        playerRb.velocity = moveDirection * speed;
    }

    void DirectionCalculate()
    { 
         moveDirection = ((Vector2)targetPos - (Vector2)transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.isOnPlatform = true;
            player.platformRb = playerRb;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.isOnPlatform = false;
        }
    }
}
