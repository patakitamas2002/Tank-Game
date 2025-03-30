using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;
    void Start()
    {
        text.text = slider.value.ToString();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    public void OnValueChanged(float value)
    {
        text.text = Math.Round(value, 3).ToString();
    }
}
