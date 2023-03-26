using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health bossHealth;
    public Slider slider;

    void Start()
    {
        slider.maxValue = bossHealth.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = bossHealth.currentHealth;
    }
}
