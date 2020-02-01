using UnityEngine;

namespace Weapon
{
    public class AssaultRifle : AbstractWeapon
    {
        [SerializeField] private Vector2 offsetRange;

        public override void FireWeapon()
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                Projectile newProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
                newProjectile.transform.Rotate(Vector3.up, Random.Range(-offsetRange.x, offsetRange.x));
                newProjectile.transform.Rotate(Vector3.right, Random.Range(-offsetRange.y, offsetRange.y));
                newProjectile.SetProjectileStats(new ProjectileStats(weaponSpecifics.ProjectileSpeed,
                    weaponSpecifics.Damage,
                    weaponSpecifics.Stun,
                    weaponSpecifics.FireRange / weaponSpecifics.ProjectileSpeed));
                cooldownTimer = weaponSpecifics.FireRate;
            }
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.ASSAULT_RIFLE;
        }
    }
}
