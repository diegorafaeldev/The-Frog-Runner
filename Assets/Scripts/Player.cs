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
    private Animator animator;

    private void Awake()
    {
        var inputPlayer = new InputControls();

        actionMove = inputPlayer.player.move;
        actionJump = inputPlayer.player.jump;

        player = GetComponent<Sprite>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
            animator.SetBool("walk", true);
        }
        else 
        {
            float linearDeceleration = Mathf.MoveTowards(rb.linearVelocity.x, 0, Deceleration * Time.deltaTime);
            rb.linearVelocity = new Vector2(linearDeceleration, rb.linearVelocityY);

            if(linearDeceleration == 0) animator.SetBool("walk", false);

        }

        if (rb.linearVelocityX > MaxSpeed)
        {
            rb.linearVelocity = new Vector2(MaxSpeed, rb.linearVelocityY);
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if (rb.linearVelocityX < -MaxSpeed) 
        { 
            rb.linearVelocity = new Vector2(-MaxSpeed, rb.linearVelocityY);
            transform.eulerAngles = new Vector3(0, 180, 0);
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
                animator.SetBool("double_jump", !dobleJumping);
            } 
            else 
            {
                if (dobleJumping) 
                {
                    rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                    dobleJumping = false;
                    animator.SetBool("double_jump", !dobleJumping);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6) 
        {
            isJumping = false;
            dobleJumping = false;
            animator.SetBool("jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = true;
            animator.SetBool("jump", true);
        }
    }
}
