using UnityEngine;

namespace Weapon
{
    public class AssaultRifle : AbstractWeapon
    {

        [SerializeField] private float offsetRange;

        public override void FireWeapon()
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                Projectile newProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
                newProjectile.SetDestroyRange(weaponSpecifics.FireRange);
                newProjectile.SetWeaponSpecifics(weaponSpecifics);
                newProjectile.transform.Rotate(Vector3.up, Random.Range(-offsetRange, offsetRange));
                newProjectile.SetVelocity(newProjectile.transform.forward);
                cooldownTimer = weaponSpecifics.FireRate;
            }
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.ASSAULT_RIFLE;
        }
    }
}
