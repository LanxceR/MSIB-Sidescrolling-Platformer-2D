using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    private MeleeAttack melee;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        melee = GetComponent<MeleeAttack>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is in range of attack and there's no attack cooldown
        if (melee.IsTargetInRange())
        {
            melee.Attack();
        }
    }
}
