using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
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
       
        if (!IsOwner)
            return;
        horizontalInput = Input.GetAxis("Horizontal");
        print(horizontalInput);
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isJumping = true;
        }
    }
    private void FixedUpdate()
    {
        if (!IsOwner)
            return;
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        isJumping = false;
    }
}

