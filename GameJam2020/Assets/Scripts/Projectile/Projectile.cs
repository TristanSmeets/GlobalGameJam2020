using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    Transform cachedTransform;
    Rigidbody rb;
    Weapon.WeaponSpecifics weaponSpecifics;

    private void OnEnable()
    {
        cachedTransform = transform;
        rb = GetComponent<Rigidbody>();
    }

    public void SetDestroyRange(float fireRange)
    {
        float destroyTime = fireRange / movementSpeed;

        Destroy(gameObject, destroyTime);
    }

    public void SetWeaponSpecifics(Weapon.WeaponSpecifics specifics)
    {
        weaponSpecifics = specifics;
    }

    public void SetVelocity(Vector3 direction)
    {
        rb.velocity = direction * movementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<AIBehavior>()?.TakeDamage(weaponSpecifics.Damage, weaponSpecifics.StunDamage);
    }
}
