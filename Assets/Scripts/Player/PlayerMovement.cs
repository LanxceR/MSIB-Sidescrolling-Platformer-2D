using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Initialization
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D col;

    private float defaultGravityScale;
    private float inputX;
    private float inputXRaw;

    // IMPORTANT: Add a short cooldown between ground checks to prevent ground checking bleeding to next frame
    // e.g. the player had moved by too little in the next frame that isGrounded() still detected the ground
    private float jumpResetCooldown = 0.1f;
    private float jumpResetTimer = 0f;

    // Walljump cooldown to lock player's movement to allow walljump/kicking
    private float wallJumpCooldown = 0.1f;
    public float wallJumpTimer = 0f;

    [Header("Movement Settings")]
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float jumpPower = 30f;

    [Header("Coyote Settings")]
    [SerializeField] private float coyoteTime;
    [SerializeField] private float wallJumpCoyoteTime;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumpCount = 1;
    [SerializeField] private int extraJumpCounter;

    [Header("Walljump Settings")]
    [SerializeField] private float wallJumpPower = 30f;
    // DO NOT USE AddForce unless physics is done in FixedUpdate()
    //[SerializeField] private float wallJumpPowerX = 2000f;
    //[SerializeField] private float wallJumpPowerY = 750f;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask oneWayPlatformLayerMask;
    [SerializeField] private LayerMask wallLayerMask;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckTransform;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        defaultGravityScale = body.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GetInstance().IsPlaying) return;

        // Fetch horizontal input => 1 = right, -1 = left
        inputX = Input.GetAxis("Horizontal");
        inputXRaw = Input.GetAxisRaw("Horizontal");

        // Jump reset timer
        jumpResetTimer = jumpResetTimer - Time.deltaTime >= 0f ? jumpResetTimer - Time.deltaTime : 0f;

        // Wall jump counter
        wallJumpTimer = wallJumpTimer - Time.deltaTime >= 0f ? wallJumpTimer - Time.deltaTime : 0f;

        // Coyote time & jump counter
        if (IsGrounded() && jumpResetTimer <= 0)
        {
            coyoteTime = 0.1f;
            extraJumpCounter = extraJumpCount;
        }
        else
        {
            coyoteTime = coyoteTime - Time.deltaTime >= 0f ? coyoteTime - Time.deltaTime : 0f;
        }

        // Wall jump coyote time
        if (IsOnWall() && !IsGrounded() && inputXRaw == transform.localScale.x && wallJumpTimer <= 0)
        {
            wallJumpCoyoteTime = 0.2f;
        }
        else
        {
            wallJumpCoyoteTime = wallJumpCoyoteTime - Time.deltaTime >= 0f ? wallJumpCoyoteTime - Time.deltaTime : 0f;
        }

        // Jump
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            Jump();
        }

        // Variable jump height
        if (((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))) && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 3);
        }

        if (wallJumpTimer <= 0)
        {
            // Hang on walls
            if (IsOnWall() && !IsGrounded() && inputXRaw == transform.localScale.x)
            {
                body.gravityScale = defaultGravityScale / 2;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = defaultGravityScale;

                // Set Player movement/velocity
                body.velocity = new Vector2(inputX * playerSpeed, body.velocity.y);
            }
        }

        //Debug.Log(IsGrounded());

        #region Sprites & Anim
        // Flip sprite
        if (inputXRaw > 0.01f) // facing right
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputXRaw < -0.01f) // facing left
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Animation
        anim.SetBool("isRunning", inputX != 0 && IsGrounded());
        anim.SetBool("isIdle", Mathf.Approximately(body.velocity.y, 0.0f));
        anim.SetBool("isOnWall", IsOnWall());
        anim.SetBool("isJumping", IsJumping());
        anim.SetBool("isFalling", IsFalling());
        #endregion
    }


    private void Jump()
    {
        // Reset jump reset timer
        jumpResetTimer = jumpResetCooldown;

        if (wallJumpCoyoteTime > 0)
        {
            WallJump();
        }
        else if (coyoteTime > 0)
        {
            // Reset coyote counter to 0 to avoid double jumping from button mashing
            coyoteTime = 0f;

            // Jump
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        } 
        else if (extraJumpCounter > 0)
        {
            // Decrement jump counter
            extraJumpCounter--;

            // Jump
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
    }

    private void WallJump()
    {
        // Wall jump
        //body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpPowerX, wallJumpPowerY));
        float x = -IsNearWall();
        body.velocity = new Vector2(x, 1f).normalized * wallJumpPower;

        Debug.Log(body.velocity);

        // Reset walljump timer
        wallJumpTimer = wallJumpCooldown;

        // Decrement jump counter
        extraJumpCounter--;

        // Reset wall jump coyote time 
        // This delay is used to prevent player from moving immediately after walljumping
        wallJumpCoyoteTime = 0f;
    }

    private bool IsJumping()
    {
        return body.velocity.y > 0.1f;
    }

    private bool IsFalling()
    {
        return body.velocity.y < -0.1f;
    }

    private bool IsGrounded()
    {
        LayerMask[] layerMasks = { groundLayerMask, oneWayPlatformLayerMask };
        RaycastHit2D hit;

        // isGrounded Check
        foreach (var layerMask in layerMasks)
        {
            //hit = Physics2D.OverlapCircle(groundCheckTransform.position, col.bounds.size.x / 2, layerMask);
            Vector2 origin = col.bounds.center;
            Vector2 size = new Vector2(col.bounds.size.x - 0.01f, col.bounds.size.y);
            hit = Physics2D.BoxCast(origin, size, 0, Vector2.down, 0.1f, layerMask);
            if (hit) return hit;
        }

        return false;
    }

    private bool IsOnWall()
    {
        // isOnWall Check
        Vector2 origin = col.bounds.center;
        Vector2 size = new Vector2(col.bounds.size.x, col.bounds.size.y - 0.5f);
        return Physics2D.BoxCast(origin, size, 0, new Vector2(transform.localScale.x, 0f), 0.1f, wallLayerMask);
    }

    private float IsNearWall()
    {
        Vector2 origin = col.bounds.center;
        Vector2 size = new Vector2(col.bounds.size.x, col.bounds.size.y - 0.5f);
        Collider2D colHitLeft = Physics2D.OverlapBox(origin - new Vector2(0.1f, 0f), size, 0, wallLayerMask);
        Collider2D colHitRight = Physics2D.OverlapBox(origin + new Vector2(0.1f, 0f), size, 0, wallLayerMask);

        // Returns -1 if there's a wall to the left, 1 if there's a wall to the right
        if (colHitLeft) return -1;
        else return 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 origin = col.bounds.center;
        Vector2 size = new Vector2(col.bounds.size.x, col.bounds.size.y - 0.5f);
        Gizmos.DrawWireCube(origin - new Vector2(0.1f, 0f), size);
        Gizmos.DrawWireCube(origin + new Vector2(0.1f, 0f), size);

        Gizmos.color = Color.blue;
        origin = col.bounds.center;
        size = new Vector2(col.bounds.size.x - 0.01f, col.bounds.size.y);
        Gizmos.DrawWireCube(origin, size);
    }
}
