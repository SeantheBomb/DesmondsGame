using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerHealthSlider : MonoBehaviour
{

    public Health health;

    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        health.OnTakeDamage += OnTakeDamage;
        health.OnHeal += OnHeal;
        slider.value = health.health / health.maxHealth;
    }

    private void OnHeal(float obj)
    {
        slider.value = health.health / health.maxHealth;
    }

    private void OnTakeDamage(DamageInfo info)
    {
        slider.value = health.health / health.maxHealth;
    }
}
