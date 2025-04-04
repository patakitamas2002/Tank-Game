private const float sidewaysFrictionFactor = 0.04f;
private void SidewaysFriction()
{
    Vector3 rightDirection = transform.right;
    float sidewaysSpeed = Vector3.Dot(rb.velocity, rightDirection);
    Vector3 sidewaysVelocity = rightDirection * sidewaysSpeed;
    Vector3 newVelocity = rb.velocity - sidewaysVelocity * sidewaysFrictionFactor;
    rb.velocity = newVelocity;
}