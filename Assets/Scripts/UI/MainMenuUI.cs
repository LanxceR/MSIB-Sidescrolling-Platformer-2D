using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject LevelSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        AudioManager.GetInstance().PlayButtonSfx();
    }

    public void BackButton()
    {
        LevelSelect.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void LevelSelectButton()
    {
        LevelSelect.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
