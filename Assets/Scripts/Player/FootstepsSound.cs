using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    [SerializeField] PlayerAnimation player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] clips;

    void Update()
    {
        if (PauseMenu.isGamePaused)
        {
            return;
        }
        if (player.currentAnimaton == "PlayerRun" )
        {
            if (!audioSource.isPlaying)
            {
                SoundManager.Instance.PlaySoundWithRandomValues(clips[Random.Range(0, clips.Length - 1)], audioSource);
            }           
        }
    }
}
