using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 15f;
    // Jump vars
    public bool onJump;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private float jumpBufferCounter;
    [SerializeField] private float legheight = 1.1f;

    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        if (jumpBufferCounter < -1)
        {
            jumpBufferCounter = -1;
        }
        PerformLateralMovement();
    }

    private void Jump()
    {
        if (CheckIfGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }

        if (jumpBufferCounter > 0 && (CheckIfGrounded() || coyoteTimeCounter > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            onJump = true;
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
        }

        jumpBufferCounter -= Time.deltaTime;
        coyoteTimeCounter -= Time.deltaTime;
    }

    private void PerformLateralMovement(){

        if(Input.GetKey(KeyCode.A)){
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        } else if(Input.GetKey(KeyCode.D)){
            rb.velocity = new Vector2(speed, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }

    bool CheckIfGrounded()
    {
        //Ray groundray = new Ray(transform.position, new Vector3(0, -1, 0));
        RaycastHit2D hit;
        if (Physics2D.Raycast(transform.position, Vector2.down, legheight))
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, legheight);
            if (hit.collider != null)
            {   
                if (hit.collider.gameObject.layer == 6) // Layer 6 is the platform prewiew layer
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {


                return false;
            }
        }
        else
        {
            return false;
        }

    }
}
