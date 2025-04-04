public void Elevate(Transform aimPoint)
{
    Vector3 targetDirection = aimPoint.position - transform.position;
    float range = Mathf.Sqrt(targetDirection.x * targetDirection.x + targetDirection.z * targetDirection.z);
    float target = Mathf.Atan2(-targetDirection.y, range) * Mathf.Rad2Deg - transform.parent.eulerAngles.x;
    if (target < -180) target += 360;
    target = Math.Clamp(target, -stats.maxElevation, stats.maxDepression);
    Quaternion trav = Quaternion.Euler(target, 0, 0);
    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, trav, stats.ElevationSpeed * Time.deltaTime);
}