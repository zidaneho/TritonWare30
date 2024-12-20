using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject winMenu;

    public bool gamePaused;
    public string MenuScene;
    public string GameScene;

    private UISoundPlayer _uiSoundPlayer;

    private void Awake()
    {
        _uiSoundPlayer = GetComponent<UISoundPlayer>();
    }

    void Start() 
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(gamePaused) 
            {
                ResumeGame();
            } 
            else 
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        _uiSoundPlayer.PlayPause();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void RestartGame()
    {
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadSceneAsync(GameScene);
    }

    public void DeathMenu()
    {
        pauseMenu.SetActive(false);
        deathMenu.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = true;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(MenuScene);
    }

    public void WinGame()
    {
        Time.timeScale = 1f;
        winMenu.SetActive(true);
    }
}
