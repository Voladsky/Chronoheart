using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] float deathDelay = 2.0f;
    

    private void OnDisable()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Invoke("LoadLevel", deathDelay);
    }

    private void LoadLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.HasKey("PlayerSaveLevel"))
            buildIndex = PlayerPrefs.GetInt("PlayerSaveLevel");
        StartCoroutine(LevelLoader.instance.LoadLevel(buildIndex));
    }
}
