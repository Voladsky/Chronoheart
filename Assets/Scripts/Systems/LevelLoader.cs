using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public Animator transition;
    public float transitionTime = 1f;
    [SerializeField] private AudioClip levelMusic;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SoundManager.Instance.PlayMusic(levelMusic);
    }
    public void LoadNextLevel()
    {
        PlayerPrefs.DeleteKey("PlayerSavePosition");
        PlayerPrefs.SetInt("PlayerSaveLevel", SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadMenu()
    {
        //PlayerPrefs.DeleteKey("PlayerSavePosition");
        StartCoroutine(LoadLevel(0));
    }
    public void LoadSameLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    public IEnumerator LoadLevel(int levelIndex)
    {

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteKey("PlayerSavePosition");
        LoadNextLevel();
    }

    public void ContinueNewGame()
    {
        int build_index = 1;
        if (PlayerPrefs.HasKey("PlayerSaveLevel"))
            build_index = PlayerPrefs.GetInt("PlayerSaveLevel");
        StartCoroutine(LoadLevel(build_index));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
