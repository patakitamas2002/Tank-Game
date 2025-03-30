using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TankManager : MonoBehaviour
{
    private Transform tank;
    public HullDatabase hulls;
    public TextMeshProUGUI hullName;

    public TurretDatabase turrets;
    public TextMeshProUGUI turretName;
    public CannonDatabase cannons;
    public TextMeshProUGUI cannonName;


    private int hullIndex = 0;
    private int turretIndex = 0;
    private int cannonIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        tank = this.transform;
        UpdateTank();
    }
    public void NextHull()
    {
        if (hullIndex + 1 < hulls.hulls.Length) hullIndex++;
        else hullIndex = 0;
        UpdateTank();
    }
    public void NextTurret()
    {
        if (turretIndex + 1 < turrets.turrets.Length) turretIndex++;
        else turretIndex = 0;
        UpdateTank();
    }
    public void NextCannon()
    {
        if (cannonIndex + 1 < cannons.cannons.Length) cannonIndex++;
        else cannonIndex = 0;
        UpdateTank();
    }
    public void PreviousHull()
    {
        if (hullIndex - 1 > 0) hullIndex--;
        else hullIndex = hulls.hulls.Length - 1;
        UpdateTank();
    }
    public void PreviousTurret()
    {
        if (turretIndex - 1 > 0) turretIndex--;
        else turretIndex = turrets.turrets.Length;
        UpdateTank();
    }
    public void PreviousCannon()
    {
        if (cannonIndex - 1 > 0) cannonIndex--;
        else cannonIndex = cannons.cannons.Length;
        UpdateTank();
    }
    private void UpdateTank()
    {
        foreach (Transform child in tank)
        {
            Destroy(child.gameObject);
        }

        GameOptions.hull = hulls.GetHull(hullIndex);
        hullName.text = GameOptions.hull.Name;
        GameObject hullModel = Instantiate(GameOptions.hull.Model, tank);
        hullModel.transform.SetParent(tank);

        GameOptions.turret = turrets.GetTurret(turretIndex);
        turretName.text = GameOptions.turret.Name;
        GameObject turretModel = Instantiate(GameOptions.turret.Model, hullModel.transform.GetChild(0));
        turretModel.transform.SetParent(tank);

        GameOptions.barrel = cannons.GetCannon(cannonIndex);
        cannonName.text = GameOptions.barrel.Name;
        GameObject cannonModel = Instantiate(GameOptions.barrel.Model, turretModel.transform.GetChild(0));
        cannonModel.transform.SetParent(tank);
    }
}
