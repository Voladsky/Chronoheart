using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Timer timer;
    [SerializeField] SpriteRenderer timerImage;

    void Update()
    {
        if (timer.CurTick)
            timerImage.sprite = sprites[0];
        else
            timerImage.sprite = sprites[1];
    }
}
