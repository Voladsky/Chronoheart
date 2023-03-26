using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] AudioClip buttonClickSound;
    [SerializeField] AudioClip buttonOnHoverSound;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                if (settingsMenu.activeSelf)
                    settingsMenu.GetComponent<SettingsMenu>().Invoke("Leave", 0);
                else
                    ResumeGame();
            }
            else PauseGame();
        }
    }
    private void PauseGame()
    {
        SoundManager.Instance.PlaySoundWithRandomValues(buttonClickSound);
        isGamePaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        SoundManager.Instance.PauseMusic();
    }
    public void ResumeGame()
    {
        SoundManager.Instance.PlaySoundWithRandomValues(buttonClickSound);
        isGamePaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        SoundManager.Instance.UnPauseMusic();
    }
    public void Restart()
    {
        SoundManager.Instance.PlaySoundWithRandomValues(buttonClickSound);
        isGamePaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        LevelLoader.instance.LoadSameLevel();
    }
    public void QuitGame()
    {
        SoundManager.Instance.PlaySoundWithRandomValues(buttonClickSound);
        PlayerPrefs.DeleteKey("PlayerSavePosition");
        isGamePaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        LevelLoader.instance.LoadMenu();
    }

    public void PlayOnHoverSound()
    {
        SoundManager.Instance.PlaySoundWithRandomValues(buttonOnHoverSound);
    }
}
