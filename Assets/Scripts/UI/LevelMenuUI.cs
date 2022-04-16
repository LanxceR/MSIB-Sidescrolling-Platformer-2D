using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelMenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI winText;

    private PlayerScore playerScore;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe methods to game manager
        GameManager.GetInstance().OnPauseAction += PauseGame;
        GameManager.GetInstance().OnResumeAction += ResumeGame;
        GameManager.GetInstance().OnLevelComplete += CompleteLevel;

        // Fetch playerScore
        playerScore = GameManager.GetInstance().ActivePlayer.GetComponent<PlayerScore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }
    public void PauseAndResumeGame()
    {
        if (GameManager.GetInstance().IsPlaying)
        {
            // Pause the game
            PauseGame();
        }
        else
        {
            // Resume the game
            ResumeGame();
        }
    }
    public void CompleteLevel()
    {
        if (playerScore)
        {
            if (playerScore.Score >= playerScore.MaxScore)
                winText.text = "Perfect!";
            else
                winText.text = "Level Complete";
        }

        winText.GetComponent<Animator>().SetTrigger("triggerEntrance");
    }
}
