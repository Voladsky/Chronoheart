using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboList : MonoBehaviour
{
    [SerializeField] Button back;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Leave();
        }
    }

    void Leave()
    {
        back.onClick.Invoke();
    }
}
