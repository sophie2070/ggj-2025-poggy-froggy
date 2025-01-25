using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float hvel;
    public float vvel;
    int jumpheight = 12;
    [SerializeField] public LayerMask dinglayer;
    bool isgrounded;
    float cytt = 2;
    float cyttc;
    BoxCollider2D bc;
    bool isfacingright = true;

    private bool isclimbing;
    private bool iswallsliding;
    private float wallslidingspeed = 2f;
    private bool iswalljumping;
    private float walljumpingdirection;
    private float walljumpingtime = 0.2f;
    private float walljumpingcounter;
    private float walljumpingduration = 0.5f;
    private Vector2 walljumpingpower = new Vector2(12f, 12f);

    private bool isdashing;
    private float dashingPower = 30;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 0.1f;
    private int currentDashes;

    public bool CanAirJump = false;

    [SerializeField] private LayerMask walllayer;
    [SerializeField] private Transform wallcheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isdashing)
        {
            return;
        }
        hvel = Input.GetAxisRaw("Horizontal");
        vvel = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && (IsTouching(dinglayer) || IsTouching(walllayer) || cyttc > 0f))
        {
            rb.AddForce(Vector2.up * jumpheight, ForceMode2D.Impulse);
        }
        if (Input.GetButtonDown("Jump") && CanAirJump == true && !(IsTouching(dinglayer)))
        {
            rb.AddForce(Vector2.up * jumpheight, ForceMode2D.Impulse);
            CanAirJump = false;
        }
        if (Input.GetButtonUp("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            cyttc = 0f;
        }
        if (Input.GetButton("Dash") && Input.GetButton("Jump"))
        {
            SuperJump();
        }
        if (Input.GetKey(KeyCode.E) && iswalled())
        {
            wallclimbing();
        }
        if (Input.GetKeyUp(KeyCode.E) && iswalled() || Input.GetKey(KeyCode.E) && !iswalled())
        {
            rb.gravityScale = 3f;
            isclimbing = false;
        }

        wallslide();
        walljump();
        if (!iswalljumping)
        {
            flip();
        }
    }
    void FixedUpdate()
    {
        if (!iswalljumping)
        {
            rb.linearVelocity = new Vector2(hvel * 10, rb.linearVelocity.y);
        }
        if (IsTouching(dinglayer) == true)
        {
            cyttc = cytt;
        }
        else
        {
            cyttc -= Time.deltaTime;
        }
    }

    void flip()
    {
        if (isfacingright && hvel < 0f || !isfacingright && hvel > 0f)
        {
            isfacingright = !isfacingright;
            Vector2 LocalScale = transform.localScale;
            LocalScale.x *= -1f;
            transform.localScale = LocalScale;
        }
    }

    private bool IsTouching(LayerMask ground)
    {
        return Physics2D.BoxCast(
                bc.bounds.center,
                bc.bounds.size,
                0f,
                Vector2.down,
                .1f,
                ground
            );
    }

    private bool iswalled()
    {
        return Physics2D.OverlapCircle(wallcheck.position, 0.1f, walllayer);
    }

    private void wallclimbing()
    {
        isclimbing = true;
        rb.gravityScale = 0f;
        if (Input.GetAxis("Vertical") != 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(vvel * 4, vvel * 4, float.MaxValue));
        }
        else if (Input.GetAxis("Vertical")  == 0)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }
    private void wallslide()
    {
        if (iswalled() && !IsTouching(dinglayer) && hvel != 0f)
        {
            iswallsliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallslidingspeed, float.MaxValue));
        }
        else
        {
            iswallsliding = false;
        }
    }

    private void SuperJump()
    {
        if (iswallsliding || isclimbing)
        {
            iswalljumping = false;
            walljumpingdirection = -transform.localScale.x;
            walljumpingcounter = walljumpingtime;
            CancelInvoke(nameof(stopwalljump));
        }
        else
        {
            walljumpingcounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && walljumpingcounter > 0f)
        {
            iswalljumping = true;
            rb.linearVelocity = new Vector2(walljumpingdirection * walljumpingpower.x * 1.4f, walljumpingpower.y * 1.25f);
            walljumpingcounter = 0f;
             
            if (transform.localScale.x != walljumpingdirection)
            {
                isfacingright = !isfacingright;
                Vector3 localscale = transform.localScale;
                localscale.x *= -1f;
                transform.localScale = localscale;
            }

            Invoke(nameof(stopwalljump), walljumpingduration);
        }
    }

    private void walljump()
    {
        if (iswallsliding || isclimbing)
        {
            iswalljumping = false;
            walljumpingdirection = -transform.localScale.x;
            walljumpingcounter = walljumpingtime;
            CancelInvoke(nameof(stopwalljump));
        }
        else
        {
            walljumpingcounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && walljumpingcounter > 0f)
        {
            iswalljumping = true;
            rb.linearVelocity = new Vector2(walljumpingdirection * walljumpingpower.x, walljumpingpower.y);
            walljumpingcounter = 0f;

            if (transform.localScale.x != walljumpingdirection)
            {
                isfacingright = !isfacingright;
                Vector3 localscale = transform.localScale;
                localscale.x *= -1f;
                transform.localScale = localscale;
            }

            Invoke(nameof(stopwalljump), walljumpingduration);
        }
    }

    private void stopwalljump()
    {
        iswalljumping = false;
    }
}