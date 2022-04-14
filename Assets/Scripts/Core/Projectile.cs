using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    BoxCollider2D boxCollider; // To disable collider when exploding
    Animator anim;
    Destroyable destroyable;
    private bool hit;
    private float direction;
    private float lifespan;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private string[] targetTags;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        destroyable = GetComponent<Destroyable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;

        transform.Translate(direction * projectileSpeed * Time.deltaTime, 0, 0);
        lifespan += Time.deltaTime;
        if (lifespan > projectileLifetime) destroyable.Deactivate();
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string tag in targetTags)
        {
            if (other.tag == tag)
            {
                // Hits something
                hit = true;
                boxCollider.enabled = false;
                anim.SetTrigger("triggerExplode");

                if (other.GetComponent<Health>())
                {
                    other.GetComponent<Health>().TakeDamage(1);
                }
            }
        }
    }

    // Set projectile direction
    public void SetDirection(float dir)
    {
        // For activating pooled objects
        gameObject.SetActive(true);
        lifespan = 0;
        hit = false;
        direction = dir;

        boxCollider.enabled = true;

        // Flip sprite
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != dir)
        {
            localScaleX *= -1;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
