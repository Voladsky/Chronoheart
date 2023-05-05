using UnityEngine;

public class LeverFan : MonoBehaviour
{
    [SerializeField] Fan fan;
    [SerializeField] bool isOff = false;
    [SerializeField] float cooldown = 0.5f;
    private float lastSwitch = -10f;

    [SerializeField] private SpriteRenderer leverSprite;
    [SerializeField] private Sprite lever1;
    [SerializeField] private Sprite lever2;

    [SerializeField] AudioClip useSound;
    [SerializeField] Door[] doors;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && Time.time - lastSwitch > cooldown)
        {
            lastSwitch = Time.time;
            if (isOff)
            {
                CloseDoors();
                fan.SetState(false);
                isOff = false;
                leverSprite.sprite = lever2;
            }
            else
            {
                OpenDoors();
                isOff = true;
                fan.SetState(true);
                leverSprite.sprite = lever1;
            }
            SoundManager.Instance.PlaySound(useSound);
        }
    }
    private void OpenDoors()
    {
        foreach (var door in doors)
        {
            door.Open();
        }
    }

    private void CloseDoors()
    {
        foreach (var door in doors)
        {
            door.Close();
        }
    }
}
