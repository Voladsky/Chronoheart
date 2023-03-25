using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBall : MonoBehaviour
{
    private void OnDisable()
    {
        //contactDamage = 0;
        Rigidbody2D rb;
        if (TryGetComponent<Rigidbody2D>(out rb))
        {
            rb.simulated = false;
        }

        //onEnemyDie.Invoke();
        GetComponent<SpriteRenderer>().sortingLayerName = "Other";
    }
}
