using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    // Initialization
    [SerializeField] private int scoreValue = 1;
    private Animator anim;
    private bool hasCollected = false;

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
        if (other.tag == "Player" && !hasCollected)
        {
            // Play sfx
            AudioManager.GetInstance().PlayItemCollecttSfx();

            // Fetch player's score
            PlayerScore playerScore = other.gameObject.GetComponent<PlayerScore>();

            // If object have no score script, return
            if (!playerScore) return;

            // Add score
            playerScore.AddScore(scoreValue);

            // Animation
            anim.SetTrigger("triggerCollected");

            // Toggle hasCollected bool
            hasCollected = true;
        }
    }
}
