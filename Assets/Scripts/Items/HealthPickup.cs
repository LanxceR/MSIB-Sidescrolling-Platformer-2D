using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Initialization
    [SerializeField] private float healthValue = 1;
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
            // Fetch player's health.cs
            Health playerHealth = other.gameObject.GetComponent<Health>();

            // If object have no health.cs, return
            if (!playerHealth) return;

            if (!playerHealth.IsOnMaxHealth())
            {
                // Play sfx
                AudioManager.GetInstance().PlayItemCollect3tSfx();

                // Heal player
                playerHealth.AddHealth(healthValue);

                // Animation
                anim.SetTrigger("triggerCollected");
            }
        }
    }
}
