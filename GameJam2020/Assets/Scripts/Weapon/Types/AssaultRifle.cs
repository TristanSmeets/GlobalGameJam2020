using UnityEngine;

namespace Weapon
{
    public class AssaultRifle : AbstractWeapon
    {
        [SerializeField] private Vector2 offsetRange = Vector2.zero;

        public override void FireWeapon()
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
                newProjectile.transform.Rotate(Vector3.up, Random.Range(-offsetRange.x, offsetRange.x));
                newProjectile.transform.Rotate(Vector3.right, Random.Range(-offsetRange.y, offsetRange.y));
                newProjectile.SetProjectileStats(projectileStats);
                cooldownTimer = weaponSpecifics.FireRate;
            }
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.ASSAULT_RIFLE;
        }
    }
}
