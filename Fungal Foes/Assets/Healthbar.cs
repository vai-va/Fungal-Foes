using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public static Healthbar instance;  // Add this line
    public Slider slider;

    private void Awake()
    {
        instance = this;  // Add this line
    }

    public void SetMaxhealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void AddHealth(int value)
    {
        slider.value += value;
    }
}
