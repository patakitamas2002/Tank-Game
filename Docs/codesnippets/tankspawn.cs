public static GameObject CreateTank(GameObject hull, GameObject turret, GameObject barrel, HealthBar hpbar, Transform transform)
{
    Tank newTank = new GameObject("Tank", typeof(Tank), typeof(Rigidbody), typeof(BoxCollider)).GetComponent<Tank>();
    newTank.transform.position = transform.position;
    newTank.hull = Instantiate(hull.GetComponent<Hull>(), newTank.transform);
    newTank.turret = Instantiate(turret.GetComponent<Turret>(), newTank.hull.transform.GetChild(0).transform);
    newTank.barrel = Instantiate(barrel.GetComponent<Barrel>(), newTank.turret.transform.GetChild(0).transform);
    newTank.healthBar = hpbar;
    return newTank.gameObject;
}