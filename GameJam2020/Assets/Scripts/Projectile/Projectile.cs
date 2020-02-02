using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileStats stats;
    private Transform cachedTransform;
    private Rigidbody rb;
    private Weapon.WeaponSpecifics weaponSpecifics;

    private void OnEnable()
    {
        cachedTransform = transform;
        rb = GetComponent<Rigidbody>();
    }

    public void SetProjectileStats(ProjectileStats stats)
    {
        this.stats = stats;
        SetVelocity(stats.MovementSpeed);
        SetDestroyTime(stats.LifeTime);
    }

    private void SetVelocity(float speed)
    {
        rb.velocity = cachedTransform.forward * speed;
    }

    private void SetDestroyTime(float timeInSeconds)
    {
        Debug.Log(timeInSeconds);
        Destroy(gameObject, timeInSeconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        AIBehavior aiBehavior = other.GetComponent<AIBehavior>();

        if(aiBehavior)
        {
            aiBehavior.TakeDamage(stats.Damage, stats.Stun);

            if (!stats.IsPiercing)
            {
                Destroy(cachedTransform.gameObject);
            }
            return;
        }
        Destroy(cachedTransform.gameObject);
    }
}
