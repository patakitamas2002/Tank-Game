public void RotateTowards(Transform aimPoint)
{
    Vector3 targetDirection = (aimPoint.position - transform.position).normalized;
    float target = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg - transform.parent.eulerAngles.y;
    Quaternion trav = Quaternion.Euler(0, target, 0);
    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, trav, stats.RotationSpeed * Time.deltaTime);
}