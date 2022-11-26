using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnDisable()
    {
        LevelLoader.instance.LoadSameLevel();
    }
}
