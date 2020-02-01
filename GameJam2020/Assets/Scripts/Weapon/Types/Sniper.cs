using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Sniper : AbstractWeapon
    {
        public override void FireWeapon()
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0)
            {
                Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
                newProjectile.SetProjectileStats(projectileStats);
                cooldownTimer = weaponSpecifics.FireRate;
            }
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.SNIPER;
        }
    }
}
