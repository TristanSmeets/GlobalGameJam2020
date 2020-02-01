using UnityEngine;

namespace Weapon
{
    public class Shotgun : AbstractWeapon
    {
        [SerializeField] private Vector2 offsetRange = Vector2.zero;
        [SerializeField] private int pellets = 10;

        private ParticleSystem particleSystem;

        private void Start()
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        public override void FireWeapon()
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer < 0)
            {
                WeaponKnockback(weaponKnockbackAmount);
                particleSystem.Play();
                for (int i = 0; i < pellets; ++i)
                {
                    Camera.main.GetComponent<CameraFollowPlayer>().ShakeCamera(0.1f, 1f);
                    Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
                    newProjectile.transform.Rotate(Vector3.up, Random.Range(-offsetRange.x, offsetRange.x));
                    newProjectile.transform.Rotate(Vector3.right, Random.Range(-offsetRange.y, offsetRange.y));
                    newProjectile.SetProjectileStats(projectileStats);
                    cooldownTimer = weaponSpecifics.FireRate;
                }
            }
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.SHOTGUN;
        }

        private void LateUpdate()
        {
            WeaponRotation();
        }
    }
}
