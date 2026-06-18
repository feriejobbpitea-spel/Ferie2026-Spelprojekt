using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 5f;
    float jumpPower = 4f;
    bool isJumping = false;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

  

     void Update()
    {
        // Kolla input för A och D knapparna
        // HorizontalInput kan vara någonstans mellan -1 och 1, där -1 är full vänster, 0 är ingen rörelse och 1 är full höger
        horizontalInput = Input.GetAxis("Horizontal");

        // Kolla om spelaren trycker på hopp-knappen och se till att spelaren inte redan är i luften (isJumping)
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isJumping = true;
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        isJumping = false;
    }
}

