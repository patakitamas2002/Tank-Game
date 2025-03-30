using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairReload : MonoBehaviour
{
    public Barrel barrel;
    Image crosshair;

    void Start()
    {
        crosshair = GetComponent<Image>();
    }

    void Update()
    {
        crosshair.fillAmount = barrel.reload / barrel.stats.ReloadTime;
    }
}
