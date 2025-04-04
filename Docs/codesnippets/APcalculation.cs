protected override float CalculatePenetration(Collision collision, float distanceTravelled)
{
    Armor hitArmor = collision.contacts[0].otherCollider.GetComponent<Armor>();
    double rad = Vector3.Angle(transform.forward, -collision.contacts[0].normal) * Mathf.Deg2Rad;
    float effectivePen = remainingPen * (float)Math.Pow(DistanceFalloff, distanceTravelled / 1000); //Distance falloff
    effectivePen = effectivePen * (float)Math.Pow(Math.Cos(rad), AnglePerformance); //Angle falloff
    return effectivePen - hitArmor.KineticResistance;
}