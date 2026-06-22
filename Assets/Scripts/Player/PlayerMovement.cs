using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Detta använder vi för att bestämma om vilka kontroller 
    // I Project Settings -> Input Manager har vi skapat "Horizontal - Player 1", "Horizontal - Player 2", "Jump - Player 1" och "Jump - Player 2"
    [SerializeField] private int PlayerID = 0;

    // För att kolla om spelaren nuddar marken
    [SerializeField] private float GroundCheckDistance = 1;
    [SerializeField] private LayerMask GroundLayerMask;

    float horizontalInput;
    float moveSpeed = 5f;
    float jumpPower = 4f;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

  

    void Update()
    {
        HandleMovement();
        HandleSpriteFlipping();
    }

    private void HandleMovement() 
    {
        // Kolla input för A och D knapparna
        // HorizontalInput kan vara någonstans mellan -1 och 1, där -1 är full vänster, 0 är ingen rörelse och 1 är full höger
        horizontalInput = Input.GetAxis($"Horizontal - Player {PlayerID}");

        // Kolla om spelaren trycker på hopp-knappen
        if (Input.GetButton($"Jump - Player {PlayerID}"))
        {
            // Hoppa uppåt om spelaren nuddar marken
            if (IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            }
        }
    }

    private void HandleSpriteFlipping() 
    {
        // Flippa karaktärens sprite ifall vi går vänster
        if (horizontalInput < 0)
            spriteRenderer.flipX = true;
        // Flippa tillbaka karaktärens sprite ifall vi går höger
        if (horizontalInput > 0)
            spriteRenderer.flipX = false;
    }


    // Skjuter en osynlig laser rakt ner och letar efter marken. Om den träffar marken så är spelaren grounded
    private bool IsGrounded() => Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, GroundLayerMask);

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x + (horizontalInput * moveSpeed), rb.linearVelocity.y);
    }
}

