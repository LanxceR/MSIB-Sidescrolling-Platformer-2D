using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuUI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe methods to game manager
        GameManager.GetInstance().OnPauseAction += PauseGame;
        GameManager.GetInstance().OnResumeAction += ResumeGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
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
}
