using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBubble : MonoBehaviour
{
    public float respawnTime = 3;
    public bool respawning = false;
    Movement PlayerMovement;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    Timer timer;

    private void Awake()
    {
        PlayerMovement = FindAnyObjectByType<Movement>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        timer = gameObject.GetComponent<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerMovement.CanAirJump = true;
            respawning = true;
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
        }
    }

    private void Update()
    {
        if(respawning)
        {
            respawnTime -= Time.deltaTime;
        }
        if(respawnTime <= 0)
        {
            spriteRenderer.enabled = true;
            circleCollider.enabled = true;
            respawning = false;
            respawnTime = 3;
        }
    }
}
