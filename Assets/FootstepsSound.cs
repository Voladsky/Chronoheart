using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    [SerializeField] PlayerAnimation player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] clips;

    [Range(0.1f, 0.5f)]
    [SerializeField]
    float volumeChangeMultiplier = 0.1f;

    [Range(0.1f, 0.5f)]
    [SerializeField]
    float pitchChangeMultiplier = 0.1f;

    void Update()
    {
        if (player.currentAnimaton == "PlayerRun" )
        {
            if (!audioSource.isPlaying)
            {
                //audioSource.volume = Random.Range(1 - volumeChangeMultiplier, 1);
                //audioSource.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
                audioSource.PlayOneShot(clips[Random.Range(0, clips.Length-1)]);
            }           
        }
    }
}
