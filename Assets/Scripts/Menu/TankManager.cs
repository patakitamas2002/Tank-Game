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
        hullIndex = hullIndex + 1 < hulls.hulls.Length ? hullIndex + 1 : 0;
        UpdateTank();
    }
    public void NextTurret()
    {
        turretIndex = turretIndex + 1 < turrets.turrets.Length ? turretIndex + 1 : 0;
        UpdateTank();
    }
    public void NextCannon()
    {
        cannonIndex = cannonIndex + 1 < cannons.cannons.Length ? cannonIndex + 1 : 0;
        UpdateTank();
    }
    public void PreviousHull()
    {
        hullIndex = hullIndex - 1 < 0 ? hulls.hulls.Length - 1 : hullIndex - 1;
        UpdateTank();
    }
    public void PreviousTurret()
    {
        turretIndex = turretIndex - 1 < 0 ? turrets.turrets.Length - 1 : turretIndex - 1;
        UpdateTank();
    }
    public void PreviousCannon()
    {
        cannonIndex = cannonIndex - 1 < 0 ? cannons.cannons.Length - 1 : cannonIndex - 1;
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
