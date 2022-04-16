using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    // Initialization
    [SerializeField] private int ammoValue = 1;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Play sfx
            AudioManager.GetInstance().PlayItemCollect2tSfx();

            // Fetch player's ranged attack script
            PlayerRangedAttack playerRanged = other.gameObject.GetComponent<PlayerRangedAttack>();

            // If object have no ranged attack script, return
            if (!playerRanged) return;

            // Replenish ammo
            playerRanged.AddAmmo(ammoValue);

            // Animation
            anim.SetTrigger("triggerCollected");
        }
    }
}
