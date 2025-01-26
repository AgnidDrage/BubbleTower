using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject prefabknife;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 15f;
    // Jump vars
    public bool onJump;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private float jumpBufferCounter;
    [SerializeField] private float legheight = 1.1f;
    private bool LastDirisLeft = true;

    private bool dynamicforward;
    [SerializeField] private float attackCooldown = 0.5f;
    private float attackCooldownCounter;
    private bool canAttack = true;
    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dynamicforward = true;
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
        RangedAttack();
        if (attackCooldownCounter > 0)
        {
            attackCooldownCounter -= Time.deltaTime;
        }
        if (attackCooldownCounter <= 0)
        {
            canAttack = true;
            attackCooldownCounter = 0;
        }

    }

    private void Jump()
    {
        if (CheckIfGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
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

    private void PerformLateralMovement()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                LastDirisLeft = true;
            }

            if(Input.GetKey(KeyCode.RightArrow))
            {
                LastDirisLeft = false;
            }
            if (!(dynamicforward == LastDirisLeft))
            {
                Flip();
                dynamicforward = LastDirisLeft;
            }

            float leftdir = 1f;
            if(dynamicforward){
                leftdir = -1f;
            }
            rb.velocity = new Vector2(speed*leftdir, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


    private void Attack()
    {

    }


    private void RangedAttack(){
      if(Input.GetKeyDown(KeyCode.S) && canAttack){
        GameObject KnifeObject = Instantiate(prefabknife,transform.position+ transform.up*1.2f,Quaternion.identity);
        int leftangle = 1;
        if(dynamicforward){
            leftangle=-1;
        }
        
        KnifeObject.GetComponent<ProjectileBehaviour>().rbk =  KnifeObject.GetComponent<Rigidbody2D>();
        KnifeObject.GetComponent<ProjectileBehaviour>().Throw(leftangle);
        attackCooldownCounter = attackCooldown;
        canAttack = false;

    }
 }
    private void Flip()
    {
        Vector3 flipedScale = transform.localScale;
        flipedScale.x *= -1;
        transform.localScale = flipedScale;
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
                if (hit.collider.gameObject.layer == 3) // Layer 6 is the platform prewiew layer
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
