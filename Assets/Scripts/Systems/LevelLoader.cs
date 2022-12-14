using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public Animator transition;
    public float transitionTime = 1f;

    private void Awake()
    {
        instance = this;
    }
    public void LoadNextLevel()
    {
        PlayerPrefs.DeleteKey("PlayerSavePosition");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadMenu()
    {
        PlayerPrefs.DeleteKey("PlayerSavePosition");
        StartCoroutine(LoadLevel(0));
    }
    public void LoadSameLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerSavePosition");
    }
}
