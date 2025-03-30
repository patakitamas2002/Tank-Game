using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpFill;
    public TextMeshProUGUI healthText;
    float healthPercent;

    public void UpdateHealth(Tank tank)
    {
        healthPercent = tank.currentHealth / tank.maxHealth;
        if (healthText != null)
            healthText.text = Mathf.Round(tank.currentHealth).ToString() + " / " + Mathf.Round(tank.maxHealth).ToString();


        hpFill.fillAmount = healthPercent;
    }

}
