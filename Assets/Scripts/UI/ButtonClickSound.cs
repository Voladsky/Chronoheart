using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    // Можно будет переделать в скрипт взаимодействия с кнопкой
    [SerializeField] AudioClip buttonClickSound;
    [SerializeField] AudioClip buttonOnHoverSound;
    public void PlayClickSound()
    {
        SoundManager.Instance.PlaySoundWithRandomValues(buttonClickSound);
    }

    public void PlayOnHoverSound()
    {
        SoundManager.Instance.PlaySoundWithRandomValues(buttonOnHoverSound);
    }
}
