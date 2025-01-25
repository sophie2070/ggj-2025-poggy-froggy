using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kwal : MonoBehaviour
{
    Movement PlayerMovement;
    int BounceHeight = 15;

    private void Awake()
    {
        PlayerMovement = FindAnyObjectByType<Movement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement.rb.AddForce(Vector2.up * BounceHeight, ForceMode2D.Impulse);
            Debug.Log("asda");
        }
    }
}
