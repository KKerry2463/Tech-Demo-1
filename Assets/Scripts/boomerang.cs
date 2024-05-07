using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerang : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxDistance = 5f;

    private GameObject player;
    [HideInInspector]public Vector2 destination;
    private bool returning = false;

    public void SetPlayer(GameObject _player)
    {
        player = _player; //Ngl this was cool, private my ass!
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime); //cosntantly go to destination.

        if (returning) // if you are told to return
        {
            destination = player.transform.position; // fucking go to the player.
        }
        if (Vector2.Distance(transform.position, player.transform.position) >= maxDistance)
        { 
            returning = true;
        }
        if (Vector2.Distance(transform.position, destination) <= 0.1f)
        {
            returning = true;
        }
        if (Vector2.Distance(transform.position, player.transform.position) <= 0.1f && returning)
        {
            player.GetComponent<playerController>().isThrown = false;
            player.GetComponent<playerController>().boomerang = gameObject;
            returning = false;
            gameObject.SetActive(false);
        }
    }
}
