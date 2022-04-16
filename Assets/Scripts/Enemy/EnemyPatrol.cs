using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private bool movingRight = true;
    [SerializeField] private float idleDuration;

    [Header("Ground detection")]
    [SerializeField] private Transform groundCheckTransform;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask oneWayPlatformLayerMask;
    [SerializeField] private LayerMask wallLayerMask;

    [Header("Misc")]
    [SerializeField] private Animator anim;

    private float idleTimer;

    private MeleeAttack melee;
    private BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        melee = GetComponent<MeleeAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (melee.IsTargetInRange())
        {
            // Animation
            anim.SetBool("isMoving", false);
            return;
        } 
        else
        {
            // Ground check & move enemy
            GetDirectionBasedOnGroundCheck();
        }
    }

    // This function is called when the behaviour becomes disabled or inactive
    private void OnDisable()
    {
        // Stops running animation
        anim.SetBool("isMoving", false);
    }

    private void GetDirectionBasedOnGroundCheck()
    {
        if (movingRight)
        {
            if (!IsGrounded() || IsFacingWall())
                ChangeDirection();
            else
                MoveInXDirection(Vector2.right.x);
        }
        else
        {
            if (!IsGrounded() || IsFacingWall())
                ChangeDirection();
            else
                MoveInXDirection(Vector2.left.x);
        }
    }

    private void ChangeDirection()
    {
        // Tick down idle timer
        idleTimer = idleTimer - Time.deltaTime < 0 ? 0 : idleTimer - Time.deltaTime;

        if (idleTimer <= 0)
        {
            // Change direction
            movingRight = !movingRight;

            // Change localScale
            int dir = movingRight ? 1 : -1;
            SetLocalScale(dir);
        }

        // Animation
        anim.SetBool("isMoving", false);
    }

    private void SetLocalScale(float direction)
    {
        // Flip sprite to face direction
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
    }

    private void MoveInXDirection(float direction)
    {
        // Reset idle timer
        idleTimer = idleDuration;

        // Change localScale
        SetLocalScale(direction);

        // Move in that direction
        transform.position = new Vector3(transform.position.x + (direction * speed * Time.deltaTime), transform.position.y, transform.position.z);

        // Animation
        anim.SetBool("isMoving", true);
    }

    private bool IsGrounded()
    {
        LayerMask[] layerMasks = { groundLayerMask, oneWayPlatformLayerMask };
        RaycastHit2D hit;

        // isGrounded Check
        foreach (var layerMask in layerMasks)
        {
            hit = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, 0.1f, layerMask);
            if (hit) return hit;
        }

        return false;
    }

    private bool IsFacingWall()
    {
        // isOnWall Check
        Vector2 origin = col.bounds.center;
        Vector2 size = new Vector2(col.bounds.size.x, col.bounds.size.y - 0.5f);
        return Physics2D.BoxCast(origin, size, 0, new Vector2(transform.localScale.x, 0f), 0.1f, wallLayerMask);
    }
}
