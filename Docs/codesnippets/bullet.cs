public abstract class Bullet : MonoBehaviour
{
    void Start()
    {
        startPosition = transform.position;
        remainingPen = GetMaxPenetration();
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Veloctiy, ForceMode.VelocityChange);
        transform.localScale = Vector3.one * Caliber / 100;
        Destroy(gameObject, maxTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (hasCollided)
        {
            return;
        }
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        hitArmor = collision.contacts[0].otherCollider.GetComponent<Armor>();
        if (hitArmor == null)
        {
            Debug.Log("Hit non-Armored object");
            hasCollided = true;
            return;
        }
        remainingPen = CalculatePenetration(collision, distanceTravelled);
        if (remainingPen <= 0)
        {
            Debug.Log("Hit armor, no Penetration left");
            hasCollided = true;
            return;
        }

        hitArmor.RegisterDamage(CalculateDMG());
        Debug.Log("Damage dealt: " + CalculateDMG());
        hasCollided = true;
    }
    protected abstract float GetMaxPenetration();
    protected abstract float CalculateDMG();
    protected abstract float CalculatePenetration(Collision collision, float distanceTravelled);

}