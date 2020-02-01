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
        public override void Fire()
        {
            cooldownTimer -= Time.deltaTime;

            if(cooldownTimer <= 0)
            {
                Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>().
                    SetDestroyRange(weaponSpecifics.FireRange);
                cooldownTimer = weaponSpecifics.FireSpeedInSeconds;
            }
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.ASSAULT_RIFLE;
        }
    }
}
