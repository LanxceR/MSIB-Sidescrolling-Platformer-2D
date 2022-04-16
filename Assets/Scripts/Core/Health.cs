using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float CurrentHealth { get; private set; }
    private bool isInvulnerable;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private Behaviour[] components;
    public UnityEvent OnHit;
    public UnityEvent OnHealthReachZero;
    public bool isDead = false;

    [Header("I-Frames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private Color flashColor;
    private Color defaultColor;

    [Header("Enemy Settings")]
    [SerializeField] private AudioSource hitSfx;
    [SerializeField] private AudioSource deathSfx;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        CurrentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("Player Health initialized");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Test Damage
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    // This function is called when the object becomes enabled and active
    private void OnEnable()
    {
        Reset();
    }


    public void TakeDamage(float damage)
    {
        if (isInvulnerable || isDead) return;

        // Deduct current health by damage, but not exceeding 0
        CurrentHealth = CurrentHealth - damage < 0 ? 0 : CurrentHealth - damage;

        if (CurrentHealth > 0)
        {
            if (gameObject.tag == "Player")
            {
                // Play sfx
                AudioManager.GetInstance().PlayHurtSfx();
            }
            else
            {
                // Play sfx
                hitSfx.Play();
            }

            OnHit?.Invoke();

            animator.SetTrigger("triggerHurt");
            StartCoroutine(Invulnerability());            
        } 
        else
        {
            // Prevent entity from dying again after its already dead
            if (!isDead)
            {
                if (gameObject.tag == "Player")
                {
                    // Play sfx
                    AudioManager.GetInstance().PlayDeathSfx();
                }
                else
                {
                    // Play sfx
                    deathSfx.Play();
                }

                animator.SetBool("isDead", true);
                animator.SetTrigger("triggerDie");

                isDead = true;

                OnHealthReachZero?.Invoke();

                foreach (Behaviour component in components)
                    component.enabled = false;   
            }
        }
    }

    public void AddHealth(float value)
    {
        // Increase health but not past max health
        CurrentHealth = CurrentHealth + value > maxHealth ? maxHealth : CurrentHealth + value;
    }

    public void Reset()
    {
        // Increase health but not past max health
        CurrentHealth = maxHealth;
        isDead = false;
    }

    public bool IsOnMaxHealth()
    {
        // Check if the player have max health or not
        return CurrentHealth == maxHealth;
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        defaultColor = spriteRenderer.color;

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = defaultColor;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        isInvulnerable = false;
        GetComponent<SpriteRenderer>().color = defaultColor;
    }
}
