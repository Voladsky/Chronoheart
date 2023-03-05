using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    // ����� ����� ���������� � ������ �������������� � �������
    [SerializeField] AudioClip buttonClickSound;

    public void PlaySound()
    {
        SoundManager.Instance.PlaySoundWithRandomValues(buttonClickSound);
    }
}
