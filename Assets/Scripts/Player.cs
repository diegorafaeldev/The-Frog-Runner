using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] public float SpeedForce = 15;
    [SerializeField] public float JumpForce = 4f;
    [SerializeField] private float MaxSpeed = 5f;
    [SerializeField] private float Deceleration = 5f;

    public bool isJumping;
    public bool dobleJumping;

    private InputAction actionMove;
    private InputAction actionJump;
    private Sprite player;

    private Rigidbody2D rb;

    private void Awake()
    {
        var inputPlayer = new InputControls();

        actionMove = inputPlayer.player.move;
        actionJump = inputPlayer.player.jump;

        player = GetComponent<Sprite>();
        rb = GetComponent<Rigidbody2D>();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void OnEnable()
    {
        actionMove.Enable(); // Habilita o InputAction
        actionJump.Enable();
    }

    private void OnDisable()
    {
        actionMove.Disable(); // Desabilita quando o objeto não estiver ativo
        actionJump.Disable();
    }

    private void Move()
    {

        float moveInputX = actionMove.ReadValue<float>();
        float movement = moveInputX * SpeedForce * Time.deltaTime;

        if (moveInputX != 0)
        {
            rb.linearVelocity = new Vector2(movement, rb.linearVelocityY);
        }
        else 
        {
            rb.linearVelocity = new Vector2(Mathf.MoveTowards(rb.linearVelocity.x, 0, Deceleration * Time.deltaTime), rb.linearVelocityY);
        }

        if (rb.linearVelocityX > MaxSpeed)
        {
            rb.linearVelocity = new Vector2(MaxSpeed, rb.linearVelocityY);
        }
        else if (rb.linearVelocityX < -MaxSpeed) 
        { 
            rb.linearVelocity = new Vector2(-MaxSpeed, rb.linearVelocityY);
        }

    }

    private void Jump()
    {
        if (actionJump.triggered)
        {
            if (!isJumping) 
            {
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                dobleJumping = true;
            } 
            else 
            {
                if (dobleJumping) 
                {
                    rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                    dobleJumping = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6) 
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = true;
        }
    }
}
