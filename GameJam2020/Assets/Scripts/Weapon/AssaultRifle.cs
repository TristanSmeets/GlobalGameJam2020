using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Weapon
{
    public class AssaultRifle : AbstractWeapon
    {
        public override void FireWeapon()
        {
            cooldownTimer -= Time.deltaTime;

            if(cooldownTimer <= 0)
            {
                Projectile newProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
                newProjectile.SetDestroyRange(weaponSpecifics.FireRange);
                newProjectile.SetWeaponSpecifics(weaponSpecifics);

                cooldownTimer = weaponSpecifics.FireRate;
            }
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.ASSAULT_RIFLE;
        }
    }
}
