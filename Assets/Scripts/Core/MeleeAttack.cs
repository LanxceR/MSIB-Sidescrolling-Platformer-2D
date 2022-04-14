using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("Attack Setting")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float range;

    [Header("Misc Settings")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float originColliderOffset;
    [SerializeField] private BoxCollider2D originCollider;

    private Vector2 origin;
    private Vector3 size;

    private Animator anim;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    public void Attack()
    {
        // Attacks
        if (cooldownTimer > attackCooldown)
        {
            anim.SetTrigger("triggerMelee");
            cooldownTimer = 0;
        }
    }

    public Collider2D IsTargetInRange()
    {
        // Check target position with raycast
        origin = originCollider.bounds.center + (transform.right * range * transform.localScale.x * originColliderOffset);
        size = new Vector3(originCollider.bounds.size.x * range, originCollider.bounds.size.y, originCollider.bounds.size.z);

        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0, Vector2.left, 0, targetLayer);

        // Check if Raycast had detected a hit
        Collider2D otherCollider = hit.collider;

        // Return Raycast result
        return otherCollider;
    }

    public void DamageTarget()
    {
        // Fetch player's collider2D
        Collider2D other;

        if (other = IsTargetInRange())
        {
            Health targetHealth = other.gameObject.GetComponent<Health>();

            if (targetHealth)
            {
                targetHealth.TakeDamage(damage);
            }
        }
    }

    // Area of attack visualization
    // Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(origin, size);
    }
}
