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
                SoundManager.Instance.PlaySoundWithRandomValues(clips[Random.Range(0, clips.Length - 1)], audioSource);
            }           
        }
    }
}
