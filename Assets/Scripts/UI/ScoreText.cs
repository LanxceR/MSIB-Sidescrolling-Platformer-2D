using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image scoreImage;
    [SerializeField] private TextMeshProUGUI scoreCounter;

    private PlayerScore playerScore;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch playerScore
        playerScore = GameManager.GetInstance().ActivePlayer.GetComponent<PlayerScore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScore)
        {
            // Update current ammo count
            scoreCounter.text = $"{playerScore.Score:00}";
        }
    }
}
