using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    [SerializeField] PlayerAnimation player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;

    void Update()
    {
        if (player.currentAnimaton == "PlayerRun" )
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clip);
            }           
        }
    }
}
