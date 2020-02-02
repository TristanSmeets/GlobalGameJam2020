using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Sniper : AbstractWeapon
    {
        private ParticleSystem particleSystem;

        private void Start()
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        private ProjectileStats _proj;

        protected override void OnEnable()
        {
            base.OnEnable();
            //projectileStats = new ProjectileStats(weaponSpecifics.ProjectileSpeed,
            //    weaponSpecifics.Damage,
            //    weaponSpecifics.Stun,
            //    weaponSpecifics.FireRange / weaponSpecifics.ProjectileSpeed,
            //    true);

            _proj = new ProjectileStats(weaponSpecifics.ProjectileSpeed,
            weaponSpecifics.Damage,
            weaponSpecifics.Stun,
            weaponSpecifics.FireRange / weaponSpecifics.ProjectileSpeed);
            cooldownTimer = weaponSpecifics.FireRate / (1 + TotalFireRateIncrease * 0.01f);
        }
        public override void FireWeapon()
        {
            cooldownTimer -= Time.deltaTime;
            if(cooldownTimer < 0)
            {
                particleSystem.Play();
                Camera.main.GetComponent<CameraFollowPlayer>().ShakeCamera(0.1f, 1.5f);
                WeaponKnockback(weaponKnockbackAmount);
                Projectile newProjectile = Instantiate(projectilePrefab,
                    projectileSpawn.position,
                    projectileSpawn.rotation).GetComponent<Projectile>();
                //newProjectile.SetProjectileStats(projectileStats);
                newProjectile.SetProjectileStats(_proj);
                //cooldownTimer = weaponSpecifics.FireRate;
                cooldownTimer = weaponSpecifics.FireRate / (1 + TotalFireRateIncrease * 0.01f);

                PlaySoundEffect(5);
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
