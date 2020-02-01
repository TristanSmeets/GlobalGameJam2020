﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Sniper : AbstractWeapon
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            projectileStats = new ProjectileStats(weaponSpecifics.ProjectileSpeed,
                weaponSpecifics.Damage,
                weaponSpecifics.Stun,
                weaponSpecifics.FireRange / weaponSpecifics.ProjectileSpeed,
                true);
        }
        public override void FireWeapon()
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0)
            {
                Camera.main.GetComponent<CameraFollowPlayer>().ShakeCamera(0.1f, 1.5f);
                WeaponKnockback(weaponKnockbackAmount);
                Projectile newProjectile = Instantiate(projectilePrefab, 
                    projectileSpawn.position, 
                    projectileSpawn.rotation).GetComponent<Projectile>();
                newProjectile.SetProjectileStats(projectileStats);
                cooldownTimer = weaponSpecifics.FireRate;
            }
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.SNIPER;
        }

        private void LateUpdate()
        {
            WeaponRotation();
        }
    }
}
