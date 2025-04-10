protected override float GetMaxPenetration()
{
    return (float)Math.Pow(ExplosiveMass, 2 / 3) / 6;
}
protected override float CalculatePenetration(Collision collision, float distanceTravelled)
{
    float armor = GetWeakestArmorThickness(collision);
    return remainingPen - armor;
}
float GetWeakestArmorThickness(Collision collision)
{
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);
    float weakest = float.MaxValue;
    for (int i = 0; i < hitColliders.Length; i++)
    {
        if (Physics.Raycast(
            transform.position, hitColliders[i].transform.position - transform.position,
            out RaycastHit hit, 10f, layerMask: ~(1 << LayerMask.NameToLayer("CollisionBox"))))
        {
            if (hit.collider.transform != hitColliders[i].transform)
                continue;
        }
        Armor armor = hitColliders[i].GetComponent<Armor>();
        if (armor == null) continue;
        float resistance = GetResistance(hitColliders[i], armor);
        if (weakest == 0)
            weakest = resistance;
        else if (resistance < weakest)
        {
            hitArmor = armor;
            weakest = resistance;
        }
    }
    return weakest;
}
float GetResistance(Collider collider, Armor armor)
{
    float distance = Vector3.Distance(transform.position, collider.ClosestPoint(transform.position));
    return armor.KineticResistance / MyMath.InvSq(distance);
}
