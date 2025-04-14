using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SaveSettings : MonoBehaviour
{
    public TextMeshProUGUI lookSens;

    public TextMeshProUGUI aimSens;

    public TextMeshProUGUI volumeSlider;

    public AudioMixer volume;
    public void SettingsSave()
    {
        PlayerSettings.TPSensitivity = float.Parse(lookSens.text);
        PlayerSettings.FPSensitivity = float.Parse(aimSens.text);
        volume.SetFloat("MasterVol", float.Parse(volumeSlider.text) * 40 - 40);
    }
}
