using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    // Detta använder vi för att bestämma om vilka kontroller 
    // I Project Settings -> Input Manager har vi skapat "Horizontal - Player 1", "Horizontal - Player 2", "Jump - Player 1" och "Jump - Player 2"
    private int PlayerID => player.PlayerID;
    private Player player;

    // För att kolla om spelaren nuddar marken
    [SerializeField] private float GroundCheckDistance = 1;
    [SerializeField] private LayerMask GroundLayerMask;
    [SerializeField] private float moveSpeed = 5.0F;
    [SerializeField] private float jumpPower = 4.0F;


    float horizontalInput;
    Vector2 externalForces;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

   

    void Update()
    {
        externalForces = Vector2.Lerp (externalForces, Vector2.zero, Time.deltaTime * 5);
        Flip();
        HandleMovement();
        
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

    public void ApplyForce(Vector2 force) 
    {
        externalForces += force;
    }

   private void Flip()
    {
        if (horizontalInput < 0)
        {
            transform.eulerAngles = new (0f, 180f, 0f);

        }
        if (horizontalInput > 0)
        {
            transform.eulerAngles = new(0f, 0, 0f);
        }


    }


    // Skjuter en osynlig laser rakt ner och letar efter marken. Om den träffar marken så är spelaren grounded
    private bool IsGrounded() => Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, GroundLayerMask);

    private void FixedUpdate()
    {
        float multipliedInput = horizontalInput * moveSpeed;
        rb.linearVelocity = new Vector2(multipliedInput + externalForces.x, rb.linearVelocity.y + externalForces.y);
    }
}

