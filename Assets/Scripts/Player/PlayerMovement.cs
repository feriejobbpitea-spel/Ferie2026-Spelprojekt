using System;
using System.Runtime.CompilerServices;
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
    [SerializeField] private float GroundCheckWidth = 1;
    [SerializeField] private LayerMask GroundLayerMask;
    [SerializeField] private float moveSpeed = 5.0F;
    [SerializeField] private float jumpPower = 4.0F;
  


    [HideInInspector]
    public float horizontalInput;
    Vector2 externalForces;
    bool hasJumped;
    bool hasAlreadyReachedPeak;

    private bool doubleJump;

    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;


    public Action OnJumped;
    public Action OnReachedPeak;
    public Action OnLanded;

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

        if (IsGrounded())
        {
            doubleJump = true;
        }

    }
  
    private void HandleMovement() 
    {
        // Kolla input för A och D knapparna
        // HorizontalInput kan vara någonstans mellan -1 och 1, där -1 är full vänster, 0 är ingen rörelse och 1 är full höger
        horizontalInput = Input.GetAxis($"Horizontal - Player {PlayerID}");
   

        if (!player.CanMove)
            horizontalInput = 0;

        // Här kollar vi om vi landat
        if(hasJumped && IsGrounded()) 
        {
            hasAlreadyReachedPeak = false;
            hasJumped = false;
            OnLanded?.Invoke();
        }

        // Här kollar vi om vi nått maximala höjden i hoppet
        // Alltså då våran velocititet på y riktningen blir negativ
        if (hasJumped && rb.linearVelocity.y < 0 && !hasAlreadyReachedPeak) 
        {
            hasAlreadyReachedPeak = true;
            OnReachedPeak?.Invoke();
        }
        
        // Kolla om spelaren trycker på hopp-knappen
        // kollar också om spelaren kan röra på sig
        if (Input.GetButtonDown($"Jump - Player {PlayerID}") && player.CanMove)
        {
            // Hoppa uppåt om spelaren nuddar marken
            if (IsGrounded() || doubleJump)
            {
                hasJumped = true;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                OnJumped?.Invoke();
                if (!IsGrounded())
                {
                    doubleJump = !doubleJump;

                }
                
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
    private bool IsGrounded() => Physics2D.OverlapBox(transform.position + Vector3.down * GroundCheckDistance, new(GroundCheckWidth, 0.1F), 0, GroundLayerMask);

    private void FixedUpdate()
    {
        float multipliedInput = horizontalInput * moveSpeed;
        rb.linearVelocity = new Vector2(multipliedInput + externalForces.x, rb.linearVelocity.y + externalForces.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + Vector3.down * GroundCheckDistance, new(GroundCheckWidth, 0.1F));
    }
}

