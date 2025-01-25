using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBubble : MonoBehaviour
{

    Movement PlayerMovement;

    private void Awake()
    {
        PlayerMovement = FindAnyObjectByType<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerMovement.CanAirJump = true;
            Destroy(this.gameObject);
        }
    }
}
