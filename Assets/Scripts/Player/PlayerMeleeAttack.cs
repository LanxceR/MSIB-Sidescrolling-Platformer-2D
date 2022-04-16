using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [Header("Keybind Settings")]
    [SerializeField] private KeyCode meleeKey;

    private MeleeAttack melee;

    // Start is called before the first frame update
    void Start()
    {
        melee = GetComponent<MeleeAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GetInstance().IsPlaying) return;

        // Check if player is in range of attack and there's no attack cooldown
        if (Input.GetKeyDown(meleeKey))
        {
            // Play sfx
            AudioManager.GetInstance().PlayMeleeSfx();

            melee.Attack();
        }
    }
}
