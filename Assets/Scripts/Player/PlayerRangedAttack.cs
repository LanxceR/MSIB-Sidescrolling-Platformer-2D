using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Main Settings")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float startingAmmo;
    [SerializeField] private PoolObjectType poolObjectType;
    public float Ammo { get; private set; }

    [Header("Keybind Settings")]
    [SerializeField] private KeyCode shootKey;

    [Header("Game Objects")]
    [SerializeField] private Transform fireTransform;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Ammo = startingAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GetInstance().IsPlaying) return;

        if (Input.GetKey(shootKey) && cooldownTimer > attackCooldown)
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    // This function is called when the object becomes enabled and active
    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        Ammo = startingAmmo;
    }


    private void Attack()
    {
        Debug.Log("Player attacks");
        anim.SetTrigger("triggerShoot");
        cooldownTimer = 0;

        // Spawn fireballs
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        if (Ammo <= 0) return;

        PoolObject projectile = ObjectPooler.GetInstance().RequestObject(poolObjectType);
        projectile.Activate(fireTransform.position);
        projectile.GetComponent<Projectile>().SetDirection(transform.localScale.x);
        Ammo--;
    }

    public void AddAmmo(int value)
    {
        Ammo += value;
    }
}
