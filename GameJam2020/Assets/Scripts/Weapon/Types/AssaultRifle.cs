﻿using UnityEngine;

namespace Weapon
{
    public class AssaultRifle : AbstractWeapon
    {
        [SerializeField] private Vector2 offsetRange = Vector2.zero;
        private ParticleSystem particleSystem;

        private ProjectileStats _proj;

        private void Start()
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();

            _proj = new ProjectileStats(weaponSpecifics.ProjectileSpeed,
            weaponSpecifics.Damage,
            weaponSpecifics.Stun,
            weaponSpecifics.FireRange / weaponSpecifics.ProjectileSpeed);
            cooldownTimer = weaponSpecifics.FireRate / (1 + TotalFireRateIncrease * 0.01f);
        }

        public override void FireWeapon()
        {
            cooldownTimer -= Time.deltaTime;

            if(cooldownTimer <= 0)
            {
                particleSystem.Play();
                WeaponKnockback(weaponKnockbackAmount);
                Camera.main.GetComponent<CameraFollowPlayer>().ShakeCamera(0.1f, 0.25f);
                Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
                newProjectile.transform.Rotate(Vector3.up, Random.Range(-offsetRange.x, offsetRange.x));
                newProjectile.transform.Rotate(Vector3.right, Random.Range(-offsetRange.y, offsetRange.y));
                //newProjectile.SetProjectileStats(projectileStats);
                newProjectile.SetProjectileStats(_proj);
                //cooldownTimer = weaponSpecifics.FireRate;
                cooldownTimer = weaponSpecifics.FireRate / (1 + TotalFireRateIncrease * 0.01f);

                PlaySoundEffect(4);
            }
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.ASSAULT_RIFLE;
        }

        private void LateUpdate()
        {
            WeaponRotation();
        }
    }
}
