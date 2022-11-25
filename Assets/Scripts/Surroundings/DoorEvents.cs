using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorEvents : MonoBehaviour
{
    [SerializeField] private List<Door> doors;
    [SerializeField] private int enemyCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyCount != 0)
        {
            foreach (var door in doors)
            {
                door.Close();
            }
        }
        gameObject.SetActive(false);
    }
    public void EnemyCountReduce()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            OpenDoors();
        }
    }
    private void OpenDoors()
    {
        foreach (var door in doors)
        {
            door.Open();
        }
    }
}
