using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioSource musicManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused) ResumeGame();
            else PauseGame();
        }
    }
    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        musicManager.Pause();
    }
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        musicManager.Play();
    }
    public void Restart()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        LevelLoader.instance.LoadSameLevel();
    }
    public void QuitGame()
    {
        PlayerPrefs.DeleteKey("PlayerSavePosition");
        isGamePaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        LevelLoader.instance.LoadMenu();
    }
}
