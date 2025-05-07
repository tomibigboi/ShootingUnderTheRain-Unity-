using UnityEngine;
using System.Collections;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 100;
    public float jumpForce = 50;
    public float dashForce = 50;
    private Vector2 velocity;
    private Vector2 lastDirection;
    private Vector2 direction;
    private bool isDashing;
    private bool isJumping;
    private bool isAir;
   
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastDirection.x = 1; 
        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(0,0);
        if (rb.linearVelocityY != 0)
        {
            isAir = true;
        }
        else
        {
            isAir = false;
        }

        // Jump and Reverse-Jump
        if (Input.GetKeyDown(KeyCode.W) && !isJumping && !isAir)
        {
            rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            isAir = true;
            StartCoroutine(resetJump());
        }
        if(Input.GetKeyDown(KeyCode.S) && isAir)
        {
            rb.AddForce(new Vector2(0, -(jumpForce * 5)), ForceMode2D.Impulse);
        }
        // Horizantal movement
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
            direction.y = 0;
            lastDirection = direction;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
            direction.y = 0;
            lastDirection = direction;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            //rb.AddForce(new Vector2(lastDirection.x * dashForce, 0), ForceMode2D.Impulse);
            StartCoroutine(PerformDash());
        }


        velocity = (speed * Time.deltaTime) * direction;
        velocity.Normalize();
        rb.AddForce(velocity);

        
        //Debug.Log(velocity);

    }

    IEnumerator PerformDash()
    {
        isDashing = true;
        rb.AddForce(new Vector2(lastDirection.x * dashForce, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("0"); 
        rb.linearVelocity = new Vector2(rb.linearVelocityX * 0.5f, rb.linearVelocityY); // Apply angular velocity to stop
        isDashing = false;
    }
    private IEnumerator resetJump()
    {
        yield return new WaitForSeconds(0.3f);
        isJumping = false;
    }


}
