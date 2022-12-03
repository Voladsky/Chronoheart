using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("PlayerSavePosition"))
            return;

        string[] pos = PlayerPrefs.GetString("PlayerSavePosition").Split('|');

        transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), 0);
    }
    void Update()
    {
        Vector3 targetPosistion = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosistion, ref velocity, smoothTime);
    }
}
