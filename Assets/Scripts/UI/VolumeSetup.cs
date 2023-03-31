using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetup : MonoBehaviour
{
    [SerializeField] SettingsMenu menu;
    void Start()
    {
        menu.SetAllVolume();
    }
}
