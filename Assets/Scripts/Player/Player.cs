using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player: MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Health health;
    [SerializeField] private RangeWeapon rangeWeapon;

    private void Awake()
    {
        
        if (!PlayerPrefs.HasKey("PlayerSavePosition" + SceneManager.GetActiveScene().buildIndex))
            return;

        string[] pos = PlayerPrefs.GetString("PlayerSavePosition" + SceneManager.GetActiveScene().buildIndex).Split('|');

        transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), 0);    
    }
    private void Start()
    {
        StartCoroutine(DecreaseHealth());       
    }

    private IEnumerator DecreaseHealth()
    {
        while (health.currentHealth > 0)
        {
            health.ReduceHealth(1);
            yield return new WaitForSeconds(1);
        }
    }
}
