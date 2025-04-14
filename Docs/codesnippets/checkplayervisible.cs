public bool CheckPlayerVisible()
{
    if (Vector3.Distance(transform.position, player.position) > 100)
        return false;
    Vector3 relativeVector = transform.InverseTransformPoint(player.position);
    float angle = Mathf.Atan2(relativeVector.x, relativeVector.z) * Mathf.Rad2Deg;
    if (Mathf.Abs(angle) > 60) return false;
    if (!Physics.Raycast(transform.position + transform.forward * 6, player.position - transform.position, out RaycastHit hit, 100, excludeCollisionBox)) return false;
    return Vector3.Distance(hit.point, player.transform.position) < 3;
}
