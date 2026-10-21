using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerControls))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerSounds))]
public class PlayerMovement : MonoBehaviour
{
    //Velocidades de movimiento del jugador
    [SerializeField]
    private float playerSpeed = 15f;
    [SerializeField]
    private float jumpForce = 2000f;

    [SerializeField]
    private float playerAditionalSpeed = 0f;
    [SerializeField]
    private float playerMaxAditionalSpeed = 1f;

    private Rigidbody2D rb;
    private PlayerControls playerControls;
    private PlayerAnimation playerAnimation;
    private PlayerSounds playerSounds;
    [SerializeField]
    private CameraFollow cameraFollow;

    //
    [SerializeField]
    private bool doubleJumpUnlocked = false;

    //Estados de movimiento del jugador
    private bool canMove = false;
    [SerializeField]
    private bool canJump = true;
    private bool canDoubleJump = false;
    private bool isGrounded = true;
    private bool isMovingHorizontal = false;

    private float sightTimer;
    private float timeToEnableSight = 0.5f;
    private bool sightingUp;
    private bool sightingDown;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer lagunaSprite;
    [SerializeField]
    private Transform lagunaTransform;

    [SerializeField]
    private Transform l1;
    [SerializeField]
    private Transform l2;

    //Input del jugador
    private Vector2 moveInput;

    void Awake()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerSounds = GetComponent<PlayerSounds>();
        playerControls = new();
    }

    void OnEnable()
    {
        playerControls.Enable();
        playerControls.Base.Jump.started += Jump;
    }

    void OnDisable()
    {
        playerControls.Disable();
        playerControls.Base.Jump.started -= Jump;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove) {
            rb.velocity = Vector2.zero;
            return;
        }

        moveInput = playerControls.Base.Movement.ReadValue<Vector2>();

        if(isMovingHorizontal)
        {
            playerAditionalSpeed = Mathf.MoveTowards(playerAditionalSpeed, playerMaxAditionalSpeed, Time.deltaTime * 3f + (playerAditionalSpeed * 0.1f));
        }
        else
        {
            playerAditionalSpeed = Mathf.MoveTowards(playerAditionalSpeed, 0f, Time.deltaTime * 5f);
        }

        //Determina si el personaje esta flipeado
        if(moveInput == Vector2.right)
        {
            playerAnimation.FlipSprite(false);
            lagunaSprite.flipX = false;
            lagunaTransform = l1;
            
        }
        else if(moveInput == Vector2.left)
        {
            playerAnimation.FlipSprite(true);
            lagunaSprite.flipX = true;
            lagunaTransform = l2;
        }

        if(rb.velocity == Vector2.zero)
        {
            if(moveInput == Vector2.up)
            {
                if(sightingDown)
                {
                    sightTimer = 0f;
                    sightingDown = false;
                    cameraFollow.ResetYOffset();
                }
                sightingUp = true;
                if(sightTimer >= timeToEnableSight)
                {
                    cameraFollow.SetYOffset(5.5f);
                }
                else
                {
                    sightTimer += Time.deltaTime;
                }
            }
            else if(moveInput == Vector2.down)
            {
                if(sightingUp)
                {
                    sightTimer = 0f;
                    sightingUp = false;
                    cameraFollow.ResetYOffset();
                }
                sightingDown = true;
                if(sightTimer >= timeToEnableSight)
                {
                    cameraFollow.SetYOffset(-0.5f);
                }
                else
                {
                    sightTimer += Time.deltaTime;
                }
            }
            else
            {
                sightTimer = 0;
                sightingUp = false;
                sightingDown = false;
                cameraFollow.ResetYOffset();
            }
        }
        else
        {
            if(sightingUp || sightingDown)
            {
                sightTimer = 0;
            }
            sightingUp = false;
            sightingDown = false;
            cameraFollow.ResetYOffset();
        }

        //Va pa arriba
        if(rb.velocity.y > 0)
        {
            playerAnimation.ChangeAnimator("Jumping", true);
            playerAnimation.ChangeAnimator("Walking", false);
            playerAnimation.ChangeAnimator("Standing", false);
            playerAnimation.ChangeAnimator("Falling", false);
            playerAnimation.ChangeAnimator("SightUp", false);
            playerAnimation.ChangeAnimator("SightDown", false);
        }
        else if(rb.velocity.y < 0)
        {
            playerAnimation.ChangeAnimator("Jumping", false);
            playerAnimation.ChangeAnimator("Walking", false);
            playerAnimation.ChangeAnimator("Standing", false);
            playerAnimation.ChangeAnimator("Falling", true);
            playerAnimation.ChangeAnimator("SightUp", false);
            playerAnimation.ChangeAnimator("SightDown", false);
        }
        //Va pa abajo
        else if((rb.velocity.x != 0) && rb.velocity.y == 0)
        {
            playerAnimation.ChangeAnimator("Jumping", false);
            playerAnimation.ChangeAnimator("Walking", true);
            playerAnimation.ChangeAnimator("Standing", false);
            playerAnimation.ChangeAnimator("Falling", false);
            playerAnimation.ChangeAnimator("SightUp", false);
            playerAnimation.ChangeAnimator("SightDown", false);
        }
        //Standing
        else if(rb.velocity == Vector2.zero)
        {
            if(moveInput == Vector2.up)
            {
                playerAnimation.ChangeAnimator("Jumping", false);
                playerAnimation.ChangeAnimator("Walking", false);
                playerAnimation.ChangeAnimator("Standing", false);
                playerAnimation.ChangeAnimator("Falling", false);
                playerAnimation.ChangeAnimator("SightUp", true);
                playerAnimation.ChangeAnimator("SightDown", false);
            }
            else if(moveInput == Vector2.down)
            {
                playerAnimation.ChangeAnimator("Jumping", false);
                playerAnimation.ChangeAnimator("Walking", false);
                playerAnimation.ChangeAnimator("Standing", false);
                playerAnimation.ChangeAnimator("Falling", false);
                playerAnimation.ChangeAnimator("SightUp", false);
                playerAnimation.ChangeAnimator("SightDown", true);
            }
            else
            {
                playerAnimation.ChangeAnimator("Jumping", false);
                playerAnimation.ChangeAnimator("Walking", false);
                playerAnimation.ChangeAnimator("Standing", true);
                playerAnimation.ChangeAnimator("Falling", false);
                playerAnimation.ChangeAnimator("SightUp", false);
                playerAnimation.ChangeAnimator("SightDown", false);
            }
        }
    }

    void FixedUpdate()
    {
        if(!canMove) {return;}

        rb.velocity = new Vector2(moveInput.x * (playerSpeed + playerAditionalSpeed), rb.velocity.y);
        isMovingHorizontal = moveInput.x != 0;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(canJump){
            AddVerticalForce(jumpForce);
            playerSounds.PlayAudioClip(0);
            canJump = false;
        }
        else if(doubleJumpUnlocked && canDoubleJump)
        {
            AddVerticalForce(jumpForce);
            playerSounds.PlayAudioClip(1);
            animator.gameObject.SetActive(true);
            animator.Play("Laguna",0,0f);
            canDoubleJump = false;
        }
    }

    private void AddVerticalForce(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, force));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            EvaluateCollision(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;

            if (normal.y >= 0.9f)
            {
                isGrounded = true;

                if (rb.velocity.y <= 0)
                {
                    ResetJump();
                }

                return;
            }
        }

    }

    private void ResetJump()
    {
        canJump = true;

        if(doubleJumpUnlocked){canDoubleJump = true;}
    }

    //Getters y setters
    public void SetDoubleJumpUnlocked(bool unlocked)
    {
        doubleJumpUnlocked = unlocked;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
