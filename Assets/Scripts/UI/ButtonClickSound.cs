using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    // Можно будет переделать в скрипт взаимодействия с кнопкой
    [SerializeField] AudioClip buttonClickSound;

    public void PlaySound()
    {
        SoundManager.Instance.PlaySoundWithRandomValues(buttonClickSound);
    }
}
