using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] MovingPlatform platform;
    [SerializeField] bool isOff = false;
    [SerializeField] float cooldown = 0.5f;
    private float lastSwitch = -10f;

    [SerializeField] private SpriteRenderer leverSprite;
    [SerializeField] private Sprite lever1;
    [SerializeField] private Sprite lever2;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && Time.time - lastSwitch > cooldown)
        {
            lastSwitch = Time.time;
            if (isOff)
            {
                platform.SetMovement(true);
                platform.Move();
                isOff = false;
                leverSprite.sprite = lever2;
            }
            else
            {
                isOff = true;
                platform.SetMovement(false);
                leverSprite.sprite = lever1;
            }
        }
    }
}
