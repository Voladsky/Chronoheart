using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] float deathDelay = 2.0f;

    private void OnDisable()
    {
        Invoke("LoadLevel", deathDelay);
    }

    private void LoadLevel()
    {
        LevelLoader.instance.LoadSameLevel();
    }
}
