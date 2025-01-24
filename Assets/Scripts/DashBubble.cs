using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBubble : MonoBehaviour
{
    Movement PlayerMovement;
    bool canDash = true;
    bool isDashing = false;
    int dashingTime = 1;
    int dashingPower = 5;

    //TrailRenderer tr;


    private void Awake()
    {
        PlayerMovement = FindAnyObjectByType<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = PlayerMovement.rb.gravityScale;
        PlayerMovement.rb.gravityScale = 0f;
        PlayerMovement.rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        PlayerMovement.rb.gravityScale = originalGravity;
        isDashing = false;
        canDash = true;
        PlayerMovement.rb.gravityScale = 4f;
    }
}
