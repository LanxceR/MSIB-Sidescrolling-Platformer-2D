using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiretrap : MonoBehaviour
{
    private enum TriggerType
    {
        AUTO, PLAYER_COL
    }

    [Header("Main Settings")]
    [SerializeField] private float offsetDelay; // Used to offset the "phase" for auto activation
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeDuration;
    [SerializeField] private TriggerType triggerType;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask playerMask = 1<<7;

    private Animator anim;
    private BoxCollider2D col;
    private EnemyContactDamage contactDmg;
    private float defaultactivationDelay;
    private bool triggered;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        contactDmg = GetComponentInChildren<EnemyContactDamage>();

        // Disable contact damage first
        contactDmg.enabled = false;

        // Set offset to activation delay for the first time only
        defaultactivationDelay = activationDelay;
        activationDelay += offsetDelay;

        // Start coroutine right away if trigger type is set to auto
        if (triggerType == TriggerType.AUTO)
            StartCoroutine(ActivateFiretrap());
    }

    // Update is called once per frame
    void Update()
    {
        // Set contact damage (fire) enabled or disabled
        contactDmg.enabled = active;

        // Animation
        anim.SetBool("isActivated", active);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactWithPlayer(other);
    }

    private void ContactWithPlayer(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && triggerType == TriggerType.PLAYER_COL)
        {
            Vector2 origin = col.bounds.center;
            Vector2 size = new Vector2(col.bounds.size.x, col.bounds.size.y);
            if (Physics2D.BoxCast(origin, size, 0, Vector2.up, 0.1f, playerMask) && !triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        while (true)
        {
            triggered = true;

            // Animation
            anim.SetTrigger("triggerWarmup");

            yield return new WaitForSeconds(activationDelay);

            active = true;

            yield return new WaitForSeconds(activeDuration);

            active = false;
            triggered = false;

            // Reset activation delay everytime
            activationDelay = defaultactivationDelay;

            // Break loop if trigger type is set other than auto
            if (triggerType != TriggerType.AUTO)
                yield break;
        }
    }
}
